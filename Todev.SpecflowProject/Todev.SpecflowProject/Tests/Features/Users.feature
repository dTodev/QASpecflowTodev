Feature: Users

Scenario: Get All users
	Given I want to prepare a request 
	When I get all users from the users endpoint
	Then The response status code should be OK
		And The response should contain a list of users

@Authenticate
Scenario Outline: Create user
	Given I have the following user data
	| Name           | Email       | Gender | Status |
	| Jerome Valeska | jjjjjjman@dc.com | Male   | Active |
	When I send a request to the users endpoint
	Then The response status code should be <statusCode>
		And the user should be created successfully

Examples:
| statusCode  |
| Created     |
| UnprocessableEntity |

@Authenticate
Scenario: Update user
	Given I have a created user in the users endpoint already
	| Id   | Name        | Email       | Gender | Status |
	| 4924 | Bruce Wayne | batman2@dc.com | male   | active |
	When I send an update request to the users endpoint
	Then The response status code should be OK
		And The user should be updated successfully

@Authenticate
Scenario: Delete user
	Given I have a created user in the users endpoint already
	| Id   | Name        | Email       | Gender | Status |
	| 5117 | Bruce Wayne | batman2@dc.com | male   | active |
	When I send a delete request to the users endpoint
	Then The response status code should be NoContent
		And the user should be deleted successfully