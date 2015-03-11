# Crucial

A WebAPI, Entity Framework, AngularJS (and ~~soon~~ now SignalR) sample project to demonstrate CQRS and Event Sourcing. 

Inspiration and adapation of code from:
- http://prodinner.codeplex.com/
- http://www.codeproject.com/Articles/555855/Introduction-to-CQRS
- http://www.asp.net/signalr/overview/getting-started/tutorial-server-broadcast-with-signalr

## Components
- AngularJS
- [ValueInjector](http://valueinjecter.codeplex.com/)
- EntityFramework 6
- WebAPI
- [StructureMap](http://docs.structuremap.net/)
- [NUnit](http://www.nunit.org/)
- [Moq](https://github.com/Moq/moq4)

# Getting started

- Install node.js & npm package manager (https://nodejs.org/download/)
- `cd ./Web`
- `npm install bower`
- `bower install`

## Update connection strings in `./API/web.config`
Databases will be created automatically as long as the connection string database server and credentials are correct

## Run
Pressing play in Visual Studio should start the Web [http://localhost:6307](http://localhost:6307) project and the API project [http://localhost:41194](http://localhost:41194)

## Grunt (Optional)
You can also optionally run the web project from the Web folder if you install grunt:
- `npm install -g grunt-cli`
- `npm install grunt`
- `npm install grunt-contrib-connect --save-dev`
You can then run `grunt connect` to view [http://localhost:8000](http://localhost:8000)

# Roadmap
- More test coverage
- More complex examples of aggregates
- Remove dependency for UI to keep track of versions
- Gracefully handle version clashes (Concurrency exceptions)
