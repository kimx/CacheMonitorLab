﻿CREATE TABLE [dbo].[MaintenanceMode](
	[MaintenanceMode] [bit] NOT NULL
) ON [PRIMARY]

GO

INSERT INTO [MaintenanceMode] ([MaintenanceMode]) VALUES (1)
GO

ALTER DATABASE MaintenanceMode SET TRUSTWORTHY ON WITH ROLLBACK IMMEDIATE
ALTER DATABASE MaintenanceMode SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE
ALTER AUTHORIZATION ON DATABASE::MaintenanceMode TO sa