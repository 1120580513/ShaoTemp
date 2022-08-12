using LetterColumnUtil = Shao.ApiTemp.Common.Utilities.Excel.LetterColumnUtil;

namespace Shao.ApiTemp.Common.UnitTests;

[TestClass]
public class ExcelTest
{
    [TestMethod]
    public void Test_LetterColumnUtil_ToLetters()
    {
        Assert.AreEqual(LetterColumnUtil.ToLetters(0), "A");
        Assert.AreEqual(LetterColumnUtil.ToLetters(25), "Z");
        Assert.AreEqual(LetterColumnUtil.ToLetters(26), "AA");
        Assert.AreEqual(LetterColumnUtil.ToLetters(51), "AZ");
        Assert.AreEqual(LetterColumnUtil.ToLetters(17576), "AAAA");
        Assert.AreEqual(LetterColumnUtil.ToLetters(17601), "AAAZ");
    }
    
    [TestMethod]
    public void Test_LetterColumnUtil_ToIndex()
    {
        Assert.AreEqual(LetterColumnUtil.ToIndex("A"), 0);
        Assert.AreEqual(LetterColumnUtil.ToIndex("Z"), 25);
        Assert.AreEqual(LetterColumnUtil.ToIndex("AA"), 26);
        Assert.AreEqual(LetterColumnUtil.ToIndex("AZ"), 51);
        Assert.AreEqual(LetterColumnUtil.ToIndex("AAAA"), 17576);
        Assert.AreEqual(LetterColumnUtil.ToIndex("AAAZ"), 17601);
    }
}