# how to build
## prepare
+ https://github.com/bigken/Gadget.git pull project from github
+ vs2017 (15.7.1) *vs升级后相应的升级了docker的版本，如果低版本的需要修改docker compose文件中的版本*
+ windows docker *linux container*
+ stylecop/resharp *recommand but not mandatory*

## build
+ run command `docker volume create gadget_mariadb_data -d local`
+ run command `docker volume create gadget_redis_data -d local`
+ run command `cd src\Gadget.Api`
+ run command `dotnet ef migrations add Initial`
+ run command `dotnet ef database update`
+ set `docker-compose` project as startup project
+ F5
+ access webapi *http://localhost:13927/swagger* in your web broswer
+ access web *http://localhost:139278* in your web broswer

## tips
+ 如果docker创建失败请确认端口是否被占用