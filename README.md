[![Build status](https://ci.appveyor.com/api/projects/status/9k8t6whmoa7sql29?svg=true)](https://ci.appveyor.com/project/h4mu/graphvisualization)
# GraphVisualization

The Loader application parses XML files in the folder given as its first argument or where it is executed in to populate a graph in the database. See the relevant unit tests for the xml structure required.

The command service is used by the loader to store the graph data in the Neo4j database.

The query service is used to query database and get the vertices and edges of the graph.

The business service can be used to determine the shortest path bethween two nodes.

The GraphVisualization project is a web application that displays the graph. If the user selects two nodes and presses the appropriate button the graph is updated with the shortest path between the two selected nodes being highlighted.

## Compilation

Build the solution either from VS or using MSBuild.

## Deployment

Tested with IIS Express, running the GraphVisualization project from VS will start all services as well.

For production-like  environments running from IIS is recommended. Copy each service's dll and config files to a folder. Map it to a virtual directory in IIS and enable asp.net for it. Update the App.config and Web.config files as necessary (database url, username, password, service endpoints, logging, etc.).

 The data services use Neo4j which has a free open source community edition. Its installer can be downloaded from neo4j.com, see http://neo4j.com/docs/operations-manual/current/ for documentation.
