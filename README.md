---
services: government
platforms: aspnetcore2.1
author: yujhong
---

# Azure Government Functions Sample
<a href="https://portal.azure.us/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fyujhongmicrosoft%2Fgov-function-sample%2Fmaster%2Fazuretest.json" target="_blank">
    <img src="http://azuredeploy.net/AzureGov.png" />
</a> 

### Description 
This sample consists of an Azure Functions application as well as a Asp.net Core Web application.
The Web application is an Image Moderator application that allows one to moderate images that are being uploaded to a Cosmos database by users. 
When a user uploads an image it is stored in a Blob container and a document referencing the image with the status "pending" is written to the Cosmos database.
A message with the Image Id is written to a Queue, which triggers the Function app which watches for the image to be uploaded.
The function sends the image to the Computer vision API to see if the image is of a Car, and returns back a status of "Approved" or "Rejected". 

# How To Run This Sample
Getting started is simple!  To run this sample in Azure Government you will need:

To run locally you will additionally need:
- Install [.NET Core](https://www.microsoft.com/net/core) 2.1.0 or later.
- Install [Visual Studio](https://www.visualstudio.com/vs/) 2017 version 15.3 or later with the following workloads:
    - **ASP.NET and web development**
    - **.NET Core cross-platform development**

### Setup

#### Step 1: Deploy Resources to Azure Government

After clicking on the "Deploy to Azure Gov" button below, you will be prompted with a ARM deployment template in the portal.  Enter the name of your choice for the App plan name parameter, and click create. 
This will set up the Azure Gov Moderator Web app. You will run the ModeratorFunctionApp locally. 
<a href="https://portal.azure.us/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fyujhongmicrosoft%2Fgov-function-sample%2Fmaster%2Fazuredeploy.json" target="_blank">
    <img src="http://azuredeploy.net/AzureGov.png" />
</a> 

#### Step 2: Provision a Computer Vision Cognitive Services account

Follow [this document](https://docs.microsoft.com/en-us/azure/azure-government/documentation-government-cognitiveservices#part-1-provision-cognitive-services-accounts) to provision your Computer Vision Cognitive Services account and retrieve your API key.

In your local.settings.json file for your Function app, add the following value:
- "VisionApiKey": "place API key here" to your Values object" 
   
#### Step 3: Set connection strings in Function App
Navigate to your deployed resources from the ARM template. 
Add the following values to your local.settings.json file:
- "storage-connection": "Enter your storage account connection string"
- "AzureWebJobsStorage": "Enter your storage account connection string"
- "GovModeratorDb": "Enter your Cosmos db connection string"
   
    
    
