# Major Project
---
This repository is intended to document the development of the project and its libraries with the project collaborators' contributions open and visible to the public.

## Project Contributors refer below steps to get started with **Backend** development

---

## Requirements
- Git
- dotNET SDK version >= 6.0
- Docker
- MongoDB docker image

## Setup 
- Install [dotnet SDK](https://dotnet.microsoft.com/en-us/download)
- Install [git](https://git-scm.com/download/win)
- Follow [these steps](https://docs.docker.com/desktop/install/windows-install/) to setup Docker on windows with WSL2 integration
- login to git with your Github account
- clone this repository and open workspace in VSCode
- run:
	```cmd
  cd backend
   dotnet restore
	```
- Once succesfully restored, launch the debug session with
  ```cmd
  dotnet watch
  ```
- The OpenAPI documentation will be available at https://localhost:5001/swagger
- If you get any https errors to access the documentation, run this and retry:
  ```ps
  dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\aspnetapp.pfx -p pass

  dotnet dev-certs https --trust
  ```

## Roadmap
- Implement user API
- Implement frontend
- Implement ticketing API
- Run individual unit tests for frontend and backend
- Deploy for final presentation

---
