# Azure CI/CD Pipeline Lab - Setup Guide

This guide will walk you through completing the Azure CI/CD Pipeline lab step by step.

## Prerequisites Checklist
- [ ] Azure account (free trial available at https://azure.microsoft.com/free/)
- [ ] GitHub account
- [ ] Azure DevOps organization (create at https://dev.azure.com/)
- [ ] Visual Studio Code or any IDE

## Part 1: Create GitHub Repository

### Step 1.1: Create New GitHub Repository
1. Go to https://github.com and sign in
2. Click the "+" icon in the top right and select "New repository"
3. Name it: `azure-function-cicd-lab`
4. Make it **Public** (for easier setup)
5. **DO NOT** initialize with README, .gitignore, or license (we already have these)
6. Click "Create repository"

### Step 1.2: Push Code to GitHub
Open your terminal/command prompt and run:

```bash
# Navigate to your lab directory
cd "path/to/your/lab/directory"

# Initialize git repository
git init

# Add all files
git add .

# Commit the files
git commit -m "Initial commit: Azure Function with CI/CD pipeline"

# Add your GitHub repository as remote (replace YOUR_USERNAME with your GitHub username)
git remote add origin https://github.com/YOUR_USERNAME/azure-function-cicd-lab.git

# Push to GitHub
git push -u origin main
```

## Part 2: Set Up Azure DevOps

### Step 2.1: Create Azure DevOps Project
1. Go to https://dev.azure.com/
2. Sign in with your Microsoft account
3. Click "New Project"
4. Fill in the details:
   - **Project name**: `AzureFunctionCICDLab`
   - **Description**: `Lab 4 - Azure CI/CD Pipeline`
   - **Visibility**: Private
   - **Version control**: Git
   - **Work item process**: Basic
5. Click "Create"

### Step 2.2: Link GitHub Repository
1. In your Azure DevOps project, go to **Pipelines**
2. Click **"New Pipeline"**
3. Choose **"GitHub"** as your source
4. Authenticate with GitHub if prompted
5. Select your repository: `azure-function-cicd-lab`
6. Choose **"Existing Azure Pipelines YAML file"**
7. Select the path: `/azure-pipelines.yml`
8. Click **"Continue"**

## Part 3: Configure Azure Resources

### Step 3.1: Create Azure Function App
1. Go to https://portal.azure.com/
2. Click **"Create a resource"**
3. Search for **"Function App"** and select it
4. Click **"Create"**
5. Fill in the details:
   - **Subscription**: Your subscription
   - **Resource Group**: Create new or use existing
   - **Function App name**: `myAzureFunctionApp` (or any unique name)
   - **Publish**: Code
   - **Runtime stack**: .NET 6 (LTS)
   - **Operating System**: Windows
   - **Plan type**: Consumption (Serverless)
   - **Region**: Choose closest to you
6. Click **"Review + create"** then **"Create"**
7. Wait for deployment to complete

### Step 3.2: Create Azure Service Connection
1. In Azure DevOps, go to **Project Settings** (bottom left)
2. Under **Pipelines**, click **"Service connections"**
3. Click **"New service connection"**
4. Choose **"Azure Resource Manager"**
5. Click **"Next"**
6. Choose **"Service principal (automatic)"**
7. Click **"Next"**
8. Select your **Subscription** and **Resource Group**
9. Give it a name: `azureSubscription`
10. Click **"Save"**

## Part 4: Update Pipeline Configuration

### Step 4.1: Update Function App Name
1. In your GitHub repository, edit the `azure-pipelines.yml` file
2. Replace `myAzureFunctionApp` with your actual Function App name (from Step 3.1)
3. Commit and push the changes

### Step 4.2: Update Service Connection Name
1. In the same `azure-pipelines.yml` file
2. Replace `$(azureSubscription)` with your actual service connection name (from Step 3.2)
3. Commit and push the changes

## Part 5: Run the Pipeline

### Step 5.1: Trigger Pipeline
1. In Azure DevOps, go to **Pipelines**
2. You should see your pipeline listed
3. Click on it and then click **"Run pipeline"**
4. Click **"Run"**

### Step 5.2: Monitor Pipeline Execution
1. Watch the pipeline run through all three stages:
   - **Build Stage**: Should complete successfully
   - **Test Stage**: Should complete successfully  
   - **Deploy Stage**: Should complete successfully
2. If any stage fails, check the logs for errors

## Part 6: Verify Deployment

### Step 6.1: Test Your Function
1. Go to Azure Portal
2. Navigate to your Function App
3. Click on **"Functions"** in the left menu
4. Click on **"HelloWorld"** function
5. Click **"Get Function URL"**
6. Copy the URL and test it in your browser or Postman
7. You should see a JSON response with "Hello, World!"

### Step 6.2: Verify Function Response
The function should return:
```json
{
  "message": "Hello, World!",
  "timestamp": "2024-01-01T12:00:00.000Z",
  "functionName": "HelloWorld"
}
```

## Part 7: Submission

### Step 7.1: Gather Required Information
1. **GitHub Repository URL**: `https://github.com/YOUR_USERNAME/azure-function-cicd-lab`
2. **Azure Function URL**: The URL you got from Step 6.1
3. **Screenshots**: Take screenshots of:
   - Pipeline execution showing all stages completed
   - Function response in browser/Postman
   - Azure Function App in Azure Portal

### Step 7.2: Submit Your Assignment
Submit the following:
- GitHub Repository URL
- Azure Function URL  
- Screenshots of pipeline execution and function response

## Troubleshooting

### Common Issues:
1. **Build fails**: Make sure .NET 6 SDK is specified correctly
2. **Test fails**: Check that all test dependencies are included
3. **Deploy fails**: Verify service connection and Function App name
4. **Function not working**: Check Function App runtime settings

### Getting Help:
- Check Azure DevOps pipeline logs for detailed error messages
- Verify all prerequisites are met
- Ensure Azure resources are properly configured

## Grading Criteria
- **Build Stage (1.5%)**: Successful compilation and packaging
- **Test Stage (1.5%)**: Running automated unit tests
- **Deploy Stage (2%)**: Successful deployment to Azure Function App

Good luck with your lab! 