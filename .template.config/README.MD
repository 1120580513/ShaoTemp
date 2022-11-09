# .template.config 为自定义模板所需要的文件

> [文档地址](https://docs.microsoft.com/zh-cn/dotnet/core/tools/custom-templates)

## template.json

|||
|--|--|
|shortName|通过命令提示符和 CLI 命令使用|
|sourceName|替换为用户指定的名称|

## 使用

1. dotnet new --install <FILE_SYSTEM_DIRECTORY>
2. dotnet new <shortName> --name <替换的名称>
3. dotnet new --uninstall <FILE_SYSTEM_DIRECTORY>