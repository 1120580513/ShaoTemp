# Shao.ApiTemp.T4

## 语法

- <#@ assembly name="<assembly_name>" #> 引入程序集
- <#@ import namespace="<namespace_name>" #> 导入命名空间
- <#@ include file="CommonHeader.txt" #> 导入文件
- 语句 <# #> 
- 表达式 <#= #>
- 铺助方法 <#+ #> 放在最后

## 结构

- Include 
  - ModelAuto.ttinclude 多文件
  - Database.ttinclude 查询数据库表结构相关
  - Custom.ttinclude 自定义方法
- Packages
  - Dapper.dll Database.ttinclude 引入

## 生成注意事项

- 可在某个 .tt 文件内直接保存即可触发生成
- VS 菜单：生成 -> 转换所有 T4 模板可生成所有
- Include 文件夹不要删除
- 除 Include 文件夹外均可直接删除
- 在使用 .tt 生成后，可直接选中所有文件夹（包括 Include，已生成后 Include 的 Dapper.dll 无法删除，所以 Include 删除会直接失效）删除

## 参考引用

- https://blog.csdn.net/liuhuan303x/article/details/85336926
- https://blog.csdn.net/EncourageMe/article/details/124866855
