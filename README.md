#SourceCodeApi

## Running the Application

This is a standard .NET 6 web app and it has an integration with a third party Api provided by GitHub.

A test GitHub user and two test repositories have been created.

- User: ebxtest
- Repositories: 
    https://github.com/ebxtest/sourcecodeapi (this is the repo of the test solution)
    https://github.com/ebxtest/ebxcontributors

The repository is publicly accessible because no authentication requirement has been specified in the AC. 
Please let me know what users would like to be added as contributors.

You can run the app using Visual Studio, the dotnet cli or in a docker container.

The API is available on the following URIs:
- http://localhost:5271
- https://localhost:7271

OpenAPI documentation is available on https://localhost:7271/swagger

## Tests

There are two type of tests, Unit and Integration.

In addition you can manually test the API using the OpenAPI docs here: https://localhost:5001/swagger

## Notes

I personally found the acceptance criteria quite generic for a test. 

Considering it's required a real integration with a third party Api (GitHub), there are few concerns I would raise before to procede with a solution.

For instance, the GitHub Api documentation is very detailed and it takes time just to figure out all the GitHub Api contracts, models and limitation. 

Also, GitHub Api provides the option to use webhooks, something I would definetely consider given some non functional requirements.

To make things simple, I have created a test account with public repositories. The AC is not very clear on this. It says an "unautorized user", but it doesn't specify if the access to the repo should be public or private.

I looked for a client library to simplify the integration and I found Octokit. It looks quite consolidated library, but I would spend more time to understand if it has any performance issue compared to the classic HttpClient solution.

I have also used MediatR as requested. I can't see the real value in it for this test, but defintely CQS can be a good way to separate concerns. In a CQRS and event driven scenario, I would separate the commands and the Queries in two different components or microservices.

I have also introduced a bit of abstraction on the SourceCode third party client, in case the API needs be extended to a different source control.

I hope my notes above make sense.

Tnanks for the opportunity



