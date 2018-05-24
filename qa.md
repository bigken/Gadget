# QA
1. VS在启动docker-compose时报*volume sharing is not enable*  
A: 参考文章[Docker for Windows, Creators Update and Volume Sharing Linux Containers](https://blogs.msdn.microsoft.com/stevelasker/2017/05/18/docker-for-windows-creators-update-and-volume-sharing-linux-containers/#comment-21085),但是我的情况是因为公司要求定期修改windows账号密码，我昨天修改过密码，所以出现这种情况，在docker/Shared Drivers/Reset credentials后就好了。


2. 在linux的docker中找不到swagger用的xml文档  
A：<GenerateDocumentationFile>true</GenerateDocumentationFile> 

