USE [asm_workorders]
GO
/****** Object:  Table [asm].[Customer]    Script Date: 1/29/2013 10:34:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [asm].[Customer](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Contact] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[JournalingId] [bigint] NULL,
 CONSTRAINT [PK_asm.Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [asm].[Vehicle]    Script Date: 1/29/2013 10:34:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [asm].[Vehicle](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Manufacturer] [nvarchar](max) NULL,
	[Model] [nvarchar](max) NULL,
	[VIN] [nvarchar](max) NULL,
	[RegistrationNumber] [nvarchar](max) NULL,
	[EngineNumber] [nvarchar](max) NULL,
	[Odometer] [bigint] NULL,
	[JournalingId] [bigint] NULL,
 CONSTRAINT [PK_asm.Vehicle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [asm].[WorkItem]    Script Date: 1/29/2013 10:34:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [asm].[WorkItem](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Unit] [nvarchar](max) NULL,
	[Value] [decimal](18, 2) NOT NULL,
	[Type] [int] NOT NULL,
	[WorkOrderId] [bigint] NOT NULL,
 CONSTRAINT [PK_asm.WorkItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [asm].[WorkOrder]    Script Date: 1/29/2013 10:34:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [asm].[WorkOrder](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Advice] [nvarchar](max) NULL,
	[Approved] [bit] NOT NULL,
	[ApprovedMaxValue] [decimal](18, 2) NULL,
	[CustomerId] [bigint] NULL,
	[CompletionDate] [datetime] NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[DateModified] [datetime] NOT NULL,
	[DateReceived] [datetime] NULL,
	[Deadline] [datetime] NULL,
	[DeliveredTo] [nvarchar](max) NULL,
	[Employee] [nvarchar](max) NULL,
	[EstimatedValue] [decimal](18, 2) NULL,
	[Number] [nvarchar](max) NULL,
	[RequestForEstimate] [bit] NOT NULL,
	[VehicleId] [bigint] NULL,
	[ApprovedMax] [bit] NOT NULL,
	[ForWorkShop] [nvarchar](max) NULL,
 CONSTRAINT [PK_asm.WorkOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [asm].[WorkOrder] ADD  DEFAULT ((0)) FOR [ApprovedMax]
GO
ALTER TABLE [asm].[WorkItem]  WITH CHECK ADD  CONSTRAINT [FK_asm.WorkItem_asm.WorkOrder_WorkOrderId] FOREIGN KEY([WorkOrderId])
REFERENCES [asm].[WorkOrder] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [asm].[WorkItem] CHECK CONSTRAINT [FK_asm.WorkItem_asm.WorkOrder_WorkOrderId]
GO
ALTER TABLE [asm].[WorkOrder]  WITH CHECK ADD  CONSTRAINT [FK_asm.WorkOrder_asm.Customer_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [asm].[Customer] ([Id])
GO
ALTER TABLE [asm].[WorkOrder] CHECK CONSTRAINT [FK_asm.WorkOrder_asm.Customer_CustomerId]
GO
ALTER TABLE [asm].[WorkOrder]  WITH CHECK ADD  CONSTRAINT [FK_asm.WorkOrder_asm.Vehicle_VehicleId] FOREIGN KEY([VehicleId])
REFERENCES [asm].[Vehicle] ([Id])
GO
ALTER TABLE [asm].[WorkOrder] CHECK CONSTRAINT [FK_asm.WorkOrder_asm.Vehicle_VehicleId]
GO

