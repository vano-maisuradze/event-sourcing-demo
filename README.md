# event-sourcing-demo
Microservices and Event Sourcing demo for [DevOps Con Tbilisi 2018](http://devopsgeorgia.ge/doct18/en/)

[Presentation URL (Georgian)](https://docs.google.com/presentation/d/1CpJcSGk7ixKKXj38ocAkMugdbueDvjzWZoeTqHg-NKE/)

### How to run the project

1. Download and install [EventStore](https://eventstore.org/)
2. Run BankAccounts.Api (Easiest way is to make BankAccounts.Api as a startup project in Visual Studio)
3. Create POST request to /api/bankaccount/create. It will create new bank account stream with BankAccountCreated event. Use the following body in request:
```json
{
	"Iban": "GE29NB0000000101904917",
	"UserId": "32b76275-4acb-4755-bb6a-e153b9fd8827",
	"Currency": 981
}
```
4. Navigate to http://127.0.0.1:2113 (default credentials are admin/changeit), navigate to "Stream Browser" tab and check if stream was created
5. Create another POST request to credit money on account /api/bankaccount/credit:
```json
{
	"AccountId": "92fc200f-c8bc-4058-9639-b5f952db2b25",
	"Amount": 100,
	"Currency": 981
}
```
Note that, value in AccountId is taken from EventStore as we want to credit money on that account. So in your case, AccountId will be different. If request was successful, you should see another event in EventStore stream (MoneyCredited).
6. Create another POST request to debit money from account /api/bankaccount/debit:
```json
{
	"AccountId": "92fc200f-c8bc-4058-9639-b5f952db2b25",
	"Amount": 70,
	"Currency": 981
}
``` 

----

You can play with Credit/Debit requests and see how new events are generated. You can also play with code and make your own changes.

----


[Here](https://github.com/vano-maisuradze/event-sourcing-demo/blob/master/Bank%20account.postman_collection.json) is the [Postman](https://www.getpostman.com/) requsts collection, so you can import and directly use it. 
