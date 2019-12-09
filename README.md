# PortableAppDeploymentCE
Deploys portable applications from web server to WIN CE

Instructions:
- Archive app folder using .ZIP
- Create a direct http download link to the .ZIP
- You must fill "AppDirectory"\Configs.txt file for this app to work, fill them as follows:

Line 1 : Path where Dc app will be deployed.
Line 2 : Full url for zip file to be downloaded.
Line 3 : Temp path for zip file.
Line 4 : Name of zip file.
