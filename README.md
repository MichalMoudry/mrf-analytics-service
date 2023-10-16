<p align="center">
    <img src="./assets/mrf_logo.svg" alt="Microservice Reference Framework logo" draggable="false" />
</p>


# Analytics service
A repository with an analytics service, that is part of Microservice Reference Framework.

## Project structure
### Service architecture
```mermaid
---
title: "Layers of the analytics service"
---
classDiagram
    class transport["Transport layer"]
    click transport href "https://github.com/MichalMoudry/mrf-workflow-service/tree/main/transport" "Go to transport layer package"
    class service["Service layer"]
    class database["Persistance layer"]
    transport <-- service
    service <-- database

    note for transport "validates requests\nparses request content\ncontains HTTP middleware\nrequest & response contracts"
    note for service "contains business logic\nhandles commiting transactions\nhandles rolling back of transactions\n..."
    note for database "contains migrations\ncontains SQL queries\ncontains repositories\nhelps with DB transaction\n..."
```
**Diagram catalog**:
- **Transport layer** - Is a layer responsible to handling incoming HTTP requests.
This means having functionality for unmarshalling request bodies, validating request data or validating JSON Web Tokens (JWTs).
- **Service layer** - This layer contains all the business logic of this service.
This can include constructing all the queries in a database transaction, publishing event to a message queue or realizing calculations on a set of data.
- **Persistance layer** - This layer is only responsible for dealing with a database.
This includes sending SQL queries to a database and retrieving responses, opening a connection to the database or providing functions for starting or commiting transactions.

## Deployment
This section contains information about analytics service's deployment process and environment.
### Deployment process
This section contains information about workflow service's deployment process and environment.
```mermaid
---
title: "Deployment of the workflow service"
---
graph TB
    start(GitHub Action trigger)
    start -- Workflow is\nmanually triggered --> manual_deploy
    start -- New tag\nis created --> version_deploy
    manual_deploy{{Manual deployment}}
    version_deploy{{New version}}

    pre_deployment((Start\npre-deployment\nscenarios))
    pre_deployment_finish((End\npre-deployment\nscenarios))
    
    manual_deploy -- Container image is\ncreated from the\napp's source code --> pre_deployment
    version_deploy -- Container image is\ncreated from the\napp's source code --> pre_deployment

    container_registery(Container\nregistery)
    db_migration(Database migration)
    pre_deployment --> container_registery
    pre_deployment --> db_migration

    db_migration -- Database scheme\nis migrated --> pre_deployment_finish
    container_registery -- App's instance\nis created from\nthe image --> pre_deployment_finish

    azure(Azure Container Apps)
    pre_deployment_finish -- Deploy a revision\nof the app --> azure
```
**Diagram catalog**:
- **GitHub Action trigger** - Starting point of the deployment process is a GitHub action for deploying the workflow service.
This action is triggered manually or when a new version/tag is created.
- **Manual deployment** - An event that represents a manual deployment of the workflow service.
- **New version** - An event representing an automatic deployment of the workflow service.
This event is triggered when a new version/tag has been created.
- **Container registry** - A registry for storing container images.
    - Examples: Docker hub or Azure Container Registry.
- **Azure Container Apps** - A cloud environment where this service is being hosted/deployed.
This environment has Dapr as a serverless service.
- **Database migration** - There is a mechanism for migrating database scheme to a new version.
This service uses so called `init container` to migrate the database.
