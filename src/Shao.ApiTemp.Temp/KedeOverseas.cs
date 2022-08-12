using Shao.ApiTemp.Common.Utilities.Excel;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace Shao.ApiTemp.Temp;

public class KedeOverseas
{
    public static void GenGoodsRecord()
    {
        using var fs = new FileStream("C:/Users/Administrator/Downloads/x.xlsx", FileMode.Open);
        var r = ExcelUtil.ReadSheet(fs, ExcelUtil.ExcelSuffix.xlsx, 0, 1);
        Debug.Assert(r.IsSucc);
        var tb = r.Data!;

        var barcodeIdx = LetterColumnUtil.ToIndex("B");
        var nameCnIdx = LetterColumnUtil.ToIndex("M");
        var nameEnIdx = LetterColumnUtil.ToIndex("N");
        var countryCodeIdx = LetterColumnUtil.ToIndex("S");

        var builder = new StringBuilder();
        foreach (DataRow row in tb.Rows)
        {
            var barcode = row[barcodeIdx].ToString();
            var nameCn = row[nameCnIdx].ToString();
            var nameEn = row[nameEnIdx].ToString();
            var countryCode = row[countryCodeIdx].ToString();
            builder.AppendFormat("('{0}','{1}','{2}','{3}')", barcode, nameEn, nameEn, countryCode);
            builder.AppendLine();
        }
        var str = builder.ToString();
        Debug.Assert(false);
    }
}
