using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Shao.ApiTemp.Common.Dto;
using System.Data;

namespace Shao.ApiTemp.Common.Utilities.Excel;

public static class ExcelUtil
{
    public static R<DataTable> ReadSheet(
        Stream stream, ExcelSuffix excelSuffix, int sheetIndex, int dataStartRowIndex = 1)
    {
        if (stream is null) return R.Fail<DataTable>($"{nameof(stream)} 参数不能为null");

        IWorkbook wordbook = InitWordbook(stream, excelSuffix);
        if (wordbook is null) return R.Fail<DataTable>($"初始化 {nameof(wordbook)} 对象失败");

        ISheet sheet = wordbook.GetSheetAt(sheetIndex);
        var dataTable = new DataTable(sheet.SheetName);

        FillSheetOfColumns(sheet, dataTable, dataStartRowIndex);
        if (dataTable.Columns.Count == default) return R.Succ(dataTable, msg: "该 Sheet 页无数据");

        FillSheetOfRows(sheet, dataTable, dataStartRowIndex);

        return R.Succ(dataTable);
    }

    public static R<DataSet> ReadExcel(
        Stream stream, ExcelSuffix excelSuffix, params int[] dataStartRowIndexs)
    {
        if (stream is null) return R.Fail<DataSet>($"{nameof(stream)} 参数不能为null");

        IWorkbook wordbook = InitWordbook(stream, excelSuffix);
        if (wordbook is null) return R.Fail<DataSet>($"初始化 {nameof(wordbook)} 对象失败");

        var dataSet = new DataSet();
        dataStartRowIndexs ??= new int[0];
        for (int sheetIdx = 0; sheetIdx < wordbook.NumberOfSheets; sheetIdx++)
        {
            var sheet = wordbook.GetSheetAt(sheetIdx);

            var dataTable = new DataTable(sheet.SheetName);
            var dataStartRowIndex = sheetIdx > (dataStartRowIndexs.Length - 1) ? 0 : dataStartRowIndexs[sheetIdx];

            FillSheetOfColumns(sheet, dataTable, dataStartRowIndex);
            dataSet.Tables.Add(dataTable);
            if (dataTable.Columns.Count == default) continue;
            FillSheetOfRows(sheet, dataTable, dataStartRowIndex);
        }

        return R.Succ(dataSet);
    }

    public static R WriteSheet(Stream stream, ExcelSuffix excelSuffix, DataTable dataTable)
    {
        if (stream is null) return R.Fail($"{nameof(stream)} 参数不能为null");
        if (dataTable is null) return R.Fail($"{nameof(dataTable)} 参数不能为null");
        if (dataTable.Columns.Count == default) return R.Fail($"{nameof(dataTable.Columns)} 不能为空");

        IWorkbook wordbook = InitWordbook(excelSuffix);
        if (wordbook is null) return R.Fail($"初始化 {nameof(wordbook)} 对象失败");

        ISheet sheet = wordbook.CreateSheet(dataTable.TableName);

        WriteSheetOfColumns(sheet, dataTable);
        WriteSheetOfRows(sheet, dataTable);

        wordbook.Write(stream);
        return R.Succ();
    }

    public static R WriteExcel(Stream stream, ExcelSuffix excelSuffix, DataSet dataSet)
    {
        if (stream is null) return R.Fail($"{nameof(stream)} 参数不能为null");
        if (dataSet is null) return R.Fail($"{nameof(dataSet)} 参数不能为null");
        if (dataSet.Tables.Count == default) return R.Fail($"{nameof(dataSet.Tables)} 不能为空");

        IWorkbook wordbook = InitWordbook(excelSuffix);
        if (wordbook is null) return R.Fail($"初始化 {nameof(wordbook)} 对象失败");

        foreach (DataTable dataTable in dataSet.Tables)
        {
            ISheet sheet = wordbook.CreateSheet(dataTable.TableName);

            WriteSheetOfColumns(sheet, dataTable);
            WriteSheetOfRows(sheet, dataTable);
        }

        wordbook.Write(stream);
        return R.Succ();
    }

    private static IWorkbook? InitWordbook(ExcelSuffix excelSuffix)
    {
        if (excelSuffix == ExcelSuffix.xlsx)
        {
            return new XSSFWorkbook();
        }
        else if (excelSuffix == ExcelSuffix.xls)
        {
            return new HSSFWorkbook();
        }

        return null;
    }

    private static IWorkbook? InitWordbook(Stream stream, ExcelSuffix excelSuffix)
    {
        if (excelSuffix == ExcelSuffix.xlsx)
        {
            return new XSSFWorkbook(stream);
        }
        else if (excelSuffix == ExcelSuffix.xls)
        {
            return new HSSFWorkbook(stream);
        }

        return null;
    }

    private static void FillSheetOfColumns(ISheet sheet, DataTable dataTable, int dataStartRowIndex)
    {
        var rowCount = sheet.LastRowNum;
        if (sheet.LastRowNum == default) return;

        var hasColumnNameRow = dataStartRowIndex > 0;
        var firstRow = sheet.GetRow(default);
        var maxCellNum = firstRow.LastCellNum;
        var dataColumns = new List<DataColumn>(maxCellNum);

        if (hasColumnNameRow)
        {// 有列名，填充列名
            for (int i = 0; i < maxCellNum; i++)
            {
                var cell = firstRow.GetCell(i);
                if (cell is null) continue;

                var columnName = GetCellStringValue(cell);
                dataColumns.Add(new DataColumn(columnName));
            }
            var hasData = dataStartRowIndex < rowCount;
            if (hasData)
            {//填充字段类型
                var dataRow = sheet.GetRow(dataStartRowIndex);
                for (int i = 0; i < maxCellNum; i++)
                {
                    var cell = dataRow.GetCell(i);
                    if (cell is null) continue;

                    var cellType = cell.CellType;
                    dataColumns[i].DataType = TypeConvert.ToType(cellType);
                }
            }
        }
        else
        {// 无列名，填充字母列名
            for (int i = 0; i < maxCellNum; i++)
            {
                var columnName = LetterColumnUtil.ToLetters(i);
                dataColumns.Add(new DataColumn(columnName));
            }
        }
        dataTable.Columns.AddRange(dataColumns.ToArray());
    }

    private static void FillSheetOfRows(ISheet sheet, DataTable dataTable, int dataStartRowIndex)
    {
        for (int rowIdx = dataStartRowIndex; rowIdx <= sheet.LastRowNum; rowIdx++)
        {
            var row = sheet.GetRow(rowIdx);
            var rowValues = new object[dataTable.Columns.Count];
            for (int colIdx = 0; colIdx < dataTable.Columns.Count; colIdx++)
            {
                var cell = row.GetCell(colIdx);
                rowValues[colIdx] = GetCellObject(cell);
            }
            dataTable.Rows.Add(rowValues);
        }
    }

    public static void WriteSheetOfColumns(ISheet sheet, DataTable dataTable)
    {
        if (dataTable.Columns.Count == default) return;
        if (string.IsNullOrWhiteSpace(dataTable.Columns[0].ColumnName)) return;

        var row = sheet.CreateRow(default(int));
        for (int colIdx = 0; colIdx < dataTable.Columns.Count; colIdx++)
        {
            var column = dataTable.Columns[colIdx];
            var cell = CreateCell(row, colIdx);
            cell.SetCellValue(column.ColumnName);
        }
    }

    public static void WriteSheetOfRows(ISheet sheet, DataTable dataTable)
    {
        var columnRowCount = 1;
        for (int rowIdx = 0; rowIdx < dataTable.Rows.Count; rowIdx++)
        {
            var row = sheet.CreateRow(rowIdx + columnRowCount);
            for (int colIdx = 0; colIdx < dataTable.Columns.Count; colIdx++)
            {
                var cell = CreateCell(row, colIdx);
                cell.SetCellValue(dataTable.Rows[rowIdx][colIdx]?.ToString());
            }
        }
    }

    private static string GetCellStringValue(ICell cell)
    {
        var obj = GetCellObject(cell);
        return obj?.ToString() ?? string.Empty;
    }

    private static object GetCellObject(ICell cell)
    {
        if (cell is null) return string.Empty;

        return cell.CellType switch
        {
            CellType.Unknown => string.Empty,
            CellType.Numeric => cell.NumericCellValue,
            CellType.String => cell.StringCellValue,
            CellType.Formula => cell.CellFormula,
            CellType.Blank => string.Empty,
            CellType.Boolean => cell.BooleanCellValue,
            CellType.Error => cell.ErrorCellValue,
            _ => string.Empty,
        };
    }

    private static ICell CreateCell(IRow row, int index)
    {
        var cell = row.CreateCell(index);
        return cell;
    }

    public static class TypeConvert
    {
        public static Type ToType(CellType cellType)
        {
            return cellType switch
            {
                CellType.Unknown => typeof(string),
                CellType.Numeric => typeof(decimal),
                CellType.String => typeof(string),
                CellType.Formula => typeof(string),
                CellType.Blank => typeof(string),
                CellType.Boolean => typeof(bool),
                CellType.Error => typeof(byte),
                _ => typeof(string),
            };
        }

        //public static CellType ToCellType(DataType dataType)
        //{
        //    // TODO
        //    return CellType.String;
        //}
    }

    public enum ExcelSuffix
    {
        xlsx,
        xls,
    }
}