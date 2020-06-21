# SesNotifications
The purpose of this application is to implement an API that can accept [AWS SES notifications through SNS notifications](https://docs.aws.amazon.com/ses/latest/DeveloperGuide/monitor-sending-activity-using-notifications-sns.html) and provide basic search facilities.

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

