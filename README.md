# Gateways

[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)
[![Tests](https://github.com/leynier/gateways-net/actions/workflows/tests.yaml/badge.svg)](https://github.com/leynier/gateways-net/actions/workflows/tests.yaml)
[![Last commit](https://img.shields.io/github/last-commit/leynier/gateways-net.svg?style=flat)](https://github.com/leynier/gateways-net/commits)
[![GitHub commit activity](https://img.shields.io/github/commit-activity/m/leynier/gateways-net)](https://github.com/leynier/gateways-net/commits)
[![Github Stars](https://img.shields.io/github/stars/leynier/gateways-net?style=flat&logo=github)](https://github.com/leynier/gateways-net/stargazers)
[![Github Forks](https://img.shields.io/github/forks/leynier/gateways-net?style=flat&logo=github)](https://github.com/leynier/gateways-net/network/members)
[![Github Watchers](https://img.shields.io/github/watchers/leynier/gateways-net?style=flat&logo=github)](https://github.com/leynier/gateways-net)
[![GitHub contributors](https://img.shields.io/github/contributors/leynier/gateways-net)](https://github.com/leynier/gateways-net/graphs/contributors)

This project is for a technical interview at a company called [Musala Soft](https://www.musala.com/).

## Description

This sample project is managing gateways - master devices that control multiple peripheral devices.

Your task is to create a REST service (JSON/HTTP) for storing information about these gateways and their associated devices.

This information must be stored in the database.

When storing a gateway, any field marked as “to be validated” must be validated and an error returned if it is invalid.

Also, no more that 10 peripheral devices are allowed for a gateway.

The service must also offer an operation for displaying information about all stored gateways (and their devices) and an operation for displaying details for a single gateway. Finally, it must be possible to add and remove a device from a gateway.

Each gateway has:

- a unique serial number (string),
- human-readable name (string),
- IPv4 address (to be validated),
- multiple associated peripheral devices.

Each peripheral device has:

- a UID (number),
- vendor (string),
- date created,
- status - online/offline.

### Other considerations

Please, provide
- Basic UI - recommended or (providing test data for Postman (or other rest client) if you do not have
enough time.
- Meaningful Unit tests.
- Readme file with installation guides.
- An automated build.

## Installation guide

- Install .Net 6.0 (<https://dotnet.microsoft.com/en-us/download>)
- Install Git (<https://git-scm.com/downloads>)
- Clone the repository ()`git clone https://github.com/leynier/gateways-net.git`)
- Navigate to the root of the repository (`cd gateways-net`)
- Install the project dependencies (`dotnet restore`)
- Build the project (`dotnet build`)
- Run the project (`dotnet run --project Gateways.Api`)
- Open browser and navigate to <https://localhost:7114/swagger>
