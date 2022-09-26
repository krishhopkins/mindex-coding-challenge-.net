## Hello, and thank you for checking out my solution!

### Please read before running:
This was the first time I dove back into C# since college(5+ years). So I apologize if any of my terminology is innacurate or confusing.

### Summary
* First issue I noticed was the existing Employee read method was not properly returning DirectReports properly. I was hoping to avoid this being an issue and to not have to deal with it, but I rapidly realized I was going to need to traverse that nested list of DirectReports to accomplish my goal. After some careful application of my Google skills, and a lot of coffee, I found the issue was in the existing Employee repository constructor. I made sure to apply this same fix down the road to the Compensation repository constructor.
* To accomplish the first goal I had to add a new object class that we don't actually want to exist in the database. The object purely exists for us to unpack the query results and inject some extra information to be returned. I then created a controller method in the existing employee controller to have a place to access our new info. I created a recursive repository helper method to count the number of reports for a given employee. I then created a service method which consumed the repository and leveraged the helper method. The final step here was populating our controller method earlier with calls to the service method we just created. 
* To accomplish the second goal I started by creating a new model totally separate from Employee, since we were going to want this model to actually relate to our db. I did my best to generally copy the skeleton of the Employee repository, controller, and dbcontext. I also made sure to register the service and repository with the service collection in App.cs. I then went ahead and created the create and read methods in the controller and hooked these back into the newly created service methods.
* Last step was to create tests for everything. I did my best to copy the existing test format and extended that to our new functionality.
### What Could I Have Done Better?
* I probably could have completed the JS assignment more quickly, but I have never felt like front-end was my forte and I'm trying to take steps in my career to move toward technologies that interest me. 
* I ran into an issue where running the tests in bulk produces a different result than running them one at a time. This is due to the internal memory DB persisting across all tests, so by the time the "numberOfReports" test runs the data has already been modified. 
* There is definitely some way to write a very happy linq query here to replace my recursive function. Leaning on query lang's to do the heavy lifting when it comes to something as trivial as getting a count of something is usually the best course of action. Instead I'm currently stuck looping recursively to get a count, which I could see leading to performance problems as the reporting chain could theorietically be infinitely long and wide. With some more practice and less general confusion on my part, I'm condfident I'd be able to get to the bottom of using the query. 

# Mindex Coding Challenge
## What's Provided
A simple [.Net 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) web application has been created and bootstrapped 
with data. The application contains information about all employees at a company. On application start-up, an in-memory 
database is bootstrapped with a serialized snapshot of the database. While the application runs, the data may be
accessed and mutated in the database without impacting the snapshot.

### How to Run
You can run this by executing `dotnet run` on the command line or in [Visual Studio Community Edition](https://www.visualstudio.com/downloads/).


### How to Use
The following endpoints are available to use:
```
* CREATE
    * HTTP Method: POST 
    * URL: localhost:8080/api/employee
    * PAYLOAD: Employee
    * RESPONSE: Employee
* READ
    * HTTP Method: GET 
    * URL: localhost:8080/api/employee/{id}
    * RESPONSE: Employee
* UPDATE
    * HTTP Method: PUT 
    * URL: localhost:8080/api/employee/{id}
    * PAYLOAD: Employee
    * RESPONSE: Employee
```
The Employee has a JSON schema of:
```json
{
  "type":"Employee",
  "properties": {
    "employeeId": {
      "type": "string"
    },
    "firstName": {
      "type": "string"
    },
    "lastName": {
          "type": "string"
    },
    "position": {
          "type": "string"
    },
    "department": {
          "type": "string"
    },
    "directReports": {
      "type": "array",
      "items" : "string"
    }
  }
}
```
For all endpoints that require an "id" in the URL, this is the "employeeId" field.

## What to Implement
Clone or download the repository, do not fork it.

### Task 1
Create a new type, ReportingStructure, that has two properties: employee and numberOfReports.

For the field "numberOfReports", this should equal the total number of reports under a given employee. The number of 
reports is determined to be the number of directReports for an employee and all of their direct reports. For example, 
given the following employee structure:
```
                    John Lennon
                /               \
         Paul McCartney         Ringo Starr
                               /        \
                          Pete Best     George Harrison
```
The numberOfReports for employee John Lennon (employeeId: 16a596ae-edd3-4847-99fe-c4518e82c86f) would be equal to 4. 

This new type should have a new REST endpoint created for it. This new endpoint should accept an employeeId and return 
the fully filled out ReportingStructure for the specified employeeId. The values should be computed on the fly and will 
not be persisted.

### Task 2
Create a new type, Compensation. A Compensation has the following fields: employee, salary, and effectiveDate. Create 
two new Compensation REST endpoints. One to create and one to read by employeeId. These should persist and query the 
Compensation from the persistence layer.

## Delivery
Please upload your results to a publicly accessible Git repo. Free ones are provided by Github and Bitbucket.