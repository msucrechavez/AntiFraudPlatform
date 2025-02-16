# AntiFraudPlatform

The AntiFraudPlatform solution contains 2 API projets:
* The AntiFraudManagerAPI: that contains the logic to validate a transaction
* The TransactionManagerAPI: that contains a list of REST resources to validate the transaction operations

## AntiFraudManagerAPI
* This project has the following flow:
  * A Kakfa consumer process a transaction event sent by the TransactionManagerAPI.
  * The transaction event is process in a use case class to apply the transaction validations
  * A redis instance is used to access the current daily amout so no request to the database are made
  * Once the transaction is validated, a new event is sent to the TransactionManagerAPI in order to udpate its status into the database

