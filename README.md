# SesNotifications
The purpose of this application is to implement an API that can accept [AWS SES notifications through SNS notifications](https://docs.aws.amazon.com/ses/latest/DeveloperGuide/monitor-sending-activity-using-notifications-sns.html) and a basic search facility.

# Requirements
* PostgreSQL database to store notifications.
* .Net core 3.1.

# Development setup
* Create the postgres database using the [init script](Sql/ses_notifications_init.sql). Apply this on an available postgres instance or use the [docker file](Docker/postgres.yml) to start a container.
* Configure the postgres location and credentials on the [application configuration file](Projects/SesNotifications.App/appsettings.json). If you use the suppplied docker file, you can omit this step.
* Start the application. You can use a utility like [Postman](https://www.postman.com/) to send raw json notifications to the api. For example json messages, see [here](https://docs.aws.amazon.com/ses/latest/DeveloperGuide/notification-examples.html).

# Production setup
* Setup a postgres database according to the method you prefer and configure the application to use it.
* Configure SNS Notifications in SES.
* Make an SNS HTTP subscription to the API using raw delivery to have AWS start sending the notifications. Alternatively use a utility like [AWS Redrive](https://github.com/nickntg/awsredrive.core) to ensure that no SNS notifications are lost.
* (Optional) Generate a Google client id and client secret and enter them in appSettings.json in order to enable Google authentication.

# Searching
On the web application, click the Queries menu and select a search page. Each search page looks up specific SES notifications and events, with the Operational page combining a subset of all notications and events in a single view.

![Menu](https://github.com/nickntg/SesNotifications/blob/master/Images/Menu.PNG)

*Contents of the search menu*

![Menu](https://github.com/nickntg/SesNotifications/blob/master/Images/View.PNG)
*The operatinal search view*

# Configuring notifications
The application provides an eventing facility that allows you to generate SQS messages whenever an incoming SES notification or event matches a pattern. This is achieved by configuring monitoring rules. You can do that by directly adding records to the *monitorrules* table or by using the ```/monitorrule``` controller of the API. A monitor rule has the following format:

* A ```Name``` that describes the rule.
* The ```SesMessage``` that defines to which type of SES notification or event this rule applies to. Valid values are ```BounceEvent```,         ```ComplaintNotification```, ```ComplaintEvent```, ```DeliveryNotification```, ```DeliveryEvent```, ```OpenEvent```, ```SendEvent```.
* A ```JsonMatcher``` that allows you to specify which part of the SES message to examine. For example, ```$.delivery.recipients``` specifies the recipients of a delivery notification.
* The ```RegEx``` to apply to the value matched by the json matcher.

If the regex produces a match, the application will send a message to an SQS queue about this event. The SQS configuration is set in the database, use the following SQL statement to set it:

```
INSERT INTO ses_notifications."configuration"
("key", value)
VALUES('sqs_notification_config', '{"queueurl": "https://sqs.eu-central-1.amazonaws.com/123456789012/your_queue","region": "eu-central-1","accesskey": "access_key","secretkey": "secret_key"}');
