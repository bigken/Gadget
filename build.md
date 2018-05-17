# how to build
## prepare
+ https://github.com/bigken/Gadget.git pull project from github
+ vs2017 (15.7.1) *vs升级后相应的升级了docker的版本，如果低版本的需要修改docker compose文件中的版本*
+ docker *linux container*
+ stylecop/resharp *recommand but not mandatory*

## build
+ run command `docker volume create gadget_mariadb_data -d local'
+ run command `docker volume create gadget_redis_data -d local`
+ run command `cd src\Gadget.Api`
+ run command `dotnet ef databases update`
+ set `docker-compose` project as startup project
+ F5
+ access *http://localhost:13927/swagger* in your web broswer

## tips
+ 如果docker创建失败请确认端口是否被占用
+ 使用dotnet ef 时当migrations/entity定义和当前运行dotnet ef的目录不是同一个时需要合并一下。反映在代码中就是
```
 services.AddDbContext<GadgetDbContext>(
                options => options.UseMySql(connectionString, b => b.MigrationsAssembly("Gadget.Api"))
            );
```