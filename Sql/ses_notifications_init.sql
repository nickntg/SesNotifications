CREATE SCHEMA ses_notifications AUTHORIZATION postgres;

CREATE TABLE ses_notifications.complaintevents (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	notification_id int8 NOT NULL,
	notification_type varchar(32) NOT NULL,
	sent_at timestamptz NOT NULL,
	message_id varchar(128) NOT NULL,
	source varchar(256) NOT NULL COLLATE "ucs_basic",
	source_arn varchar(256) NULL,
	source_ip varchar(32) NULL,
	sending_account_id varchar(128) NULL,
	created_at timestamptz NULL,
	complaint_sub_type varchar(64) NULL COLLATE "ucs_basic",
	complaint_feedback_type varchar(256) NULL COLLATE "ucs_basic",
	feedback_id varchar(256) NULL,
	complained_recipients varchar(64000) NULL COLLATE "ucs_basic",
	arrival_date timestamptz NULL
);
CREATE INDEX complaintevents_complained_recipients_idx ON ses_notifications.complaintevents (complained_recipients);
CREATE INDEX complaintevents_complaint_feedback_type_idx ON ses_notifications.complaintevents (complaint_feedback_type);
CREATE INDEX complaintevents_complaint_sub_type_idx ON ses_notifications.complaintevents (complaint_sub_type);
CREATE INDEX complaintevents_created_at_idx ON ses_notifications.complaintevents (created_at);
CREATE INDEX complaintevents_from_idx ON ses_notifications.complaintevents (source);
CREATE UNIQUE INDEX complaintevents_id_idx ON ses_notifications.complaintevents (id);
CREATE INDEX complaintevents_message_id_idx ON ses_notifications.complaintevents (message_id);
CREATE INDEX complaintevents_notification_id_idx ON ses_notifications.complaintevents (notification_id);
CREATE INDEX complaintevents_sent_at_idx ON ses_notifications.complaintevents (sent_at);

CREATE TABLE ses_notifications.bounceevents (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	notification_id int8 NOT NULL,
	notification_type varchar(32) NOT NULL,
	sent_at timestamptz NOT NULL,
	message_id varchar(128) NOT NULL,
	source varchar(256) NOT NULL COLLATE "ucs_basic",
	source_arn varchar(256) NULL,
	source_ip varchar(32) NULL,
	sending_account_id varchar(128) NULL,
	bounce_type varchar(128) NOT NULL,
	bounce_sub_type varchar(128) NOT NULL,
	created_at timestamptz NULL,
	feedback_id varchar(256) NULL,
	reporting_mta varchar(256) NULL,
	bounced_recipients varchar(64000) NULL COLLATE "ucs_basic"
);
CREATE INDEX bounceevents_bounce_sub_type_idx ON ses_notifications.bounceevents USING btree (bounce_sub_type);
CREATE INDEX bounceevents_bounce_type_idx ON ses_notifications.bounceevents USING btree (bounce_type);
CREATE INDEX bounceevents_bounced_recipients_idx ON ses_notifications.bounceevents USING btree (bounced_recipients);
CREATE INDEX bounceevents_created_at_idx ON ses_notifications.bounceevents USING btree (created_at);
CREATE INDEX bounceevents_from_idx ON ses_notifications.bounceevents USING btree (source);
CREATE UNIQUE INDEX bounceevents_id_idx ON ses_notifications.bounceevents USING btree (id);
CREATE INDEX bounceevents_message_id_idx ON ses_notifications.bounceevents USING btree (message_id);
CREATE INDEX bounceevents_notification_id_idx ON ses_notifications.bounceevents USING btree (notification_id);
CREATE INDEX bounceevents_sent_at_idx ON ses_notifications.bounceevents USING btree (sent_at);

CREATE TABLE ses_notifications.deliveryevents (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	notification_id int8 NOT NULL,
	notification_type varchar(32) NOT NULL,
	sent_at timestamptz NOT NULL,
	message_id varchar(128) NOT NULL,
	source varchar(256) NOT NULL COLLATE "ucs_basic",
	source_arn varchar(256) NULL,
	source_ip varchar(32) NULL,
	sending_account_id varchar(128) NULL,
	delivered_at timestamptz NULL,
	smtp_response varchar(4000) NULL COLLATE "ucs_basic",
	reporting_mta varchar(256) NULL,
	recipients varchar(64000) NULL COLLATE "ucs_basic"
);
CREATE INDEX deliveryevents_delivered_at_idx ON ses_notifications.deliveryevents (delivered_at);
CREATE INDEX deliveryevents_from_idx ON ses_notifications.deliveryevents (source);
CREATE UNIQUE INDEX deliveryevents_id_idx ON ses_notifications.deliveryevents (id);
CREATE INDEX deliveryevents_message_id_idx ON ses_notifications.deliveryevents (message_id);
CREATE INDEX deliveryevents_notification_id_idx ON ses_notifications.deliveryevents (notification_id);
CREATE INDEX deliveryevents_recipients_idx ON ses_notifications.deliveryevents (recipients);
CREATE INDEX deliveryevents_sent_at_idx ON ses_notifications.deliveryevents (sent_at);
CREATE INDEX deliveryevents_smtp_response_idx ON ses_notifications.deliveryevents (smtp_response);

CREATE TABLE ses_notifications.openevents (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	notification_id int8 NOT NULL,
	notification_type varchar(32) NOT NULL,
	sent_at timestamptz NOT NULL,
	message_id varchar(128) NOT NULL,
	source varchar(256) NOT NULL COLLATE "ucs_basic",
	source_arn varchar(256) NULL,
	source_ip varchar(32) NULL,
	sending_account_id varchar(128) NULL,
	recipients varchar(64000) NULL COLLATE "ucs_basic",
	opened_at timestamptz NULL,
	user_agent varchar(1024) NULL,
	ip_address varchar(128) NULL
);
CREATE INDEX openevents_recipients_idx ON ses_notifications.openevents USING btree (recipients);
CREATE INDEX openevents_from_idx ON ses_notifications.openevents USING btree (source);
CREATE UNIQUE INDEX openevents_id_idx ON ses_notifications.openevents USING btree (id);
CREATE INDEX openevents_message_id_idx ON ses_notifications.openevents USING btree (message_id);
CREATE INDEX openevents_notification_id_idx ON ses_notifications.openevents USING btree (notification_id);
CREATE INDEX openevents_sent_at_idx ON ses_notifications.openevents USING btree (sent_at);

CREATE TABLE ses_notifications.sendevents (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	notification_id int8 NOT NULL,
	notification_type varchar(32) NOT NULL,
	sent_at timestamptz NOT NULL,
	message_id varchar(128) NOT NULL,
	source varchar(256) NOT NULL COLLATE "ucs_basic",
	source_arn varchar(256) NULL,
	source_ip varchar(32) NULL,
	sending_account_id varchar(128) NULL,
	recipients varchar(64000) NULL COLLATE "ucs_basic"
);
CREATE INDEX sendevents_recipients_idx ON ses_notifications.sendevents USING btree (recipients);
CREATE INDEX sendevents_from_idx ON ses_notifications.sendevents USING btree (source);
CREATE UNIQUE INDEX sendevents_id_idx ON ses_notifications.sendevents USING btree (id);
CREATE INDEX sendevents_message_id_idx ON ses_notifications.sendevents USING btree (message_id);
CREATE INDEX sendevents_notification_id_idx ON ses_notifications.sendevents USING btree (notification_id);

CREATE TABLE ses_notifications.bounces (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	notification_id int8 NOT NULL,
	notification_type varchar(32) NOT NULL,
	sent_at timestamptz NOT NULL,
	message_id varchar(128) NOT NULL,
	source varchar(256) NOT NULL COLLATE "ucs_basic",
	source_arn varchar(256) NULL,
	source_ip varchar(32) NULL,
	sending_account_id varchar(128) NULL,
	bounce_type varchar(128) NOT NULL,
	bounce_sub_type varchar(128) NOT NULL,
	created_at timestamptz NULL,
	feedback_id varchar(256) NULL,
	reporting_mta varchar(256) NULL,
	remote_mta_ip varchar(32) NULL,
	bounced_recipients varchar(64000) NULL COLLATE "ucs_basic"
);
CREATE INDEX bounces_bounce_sub_type_idx ON ses_notifications.bounces USING btree (bounce_sub_type);
CREATE INDEX bounces_bounce_type_idx ON ses_notifications.bounces USING btree (bounce_type);
CREATE INDEX bounces_bounced_recipients_idx ON ses_notifications.bounces USING btree (bounced_recipients);
CREATE INDEX bounces_created_at_idx ON ses_notifications.bounces USING btree (created_at);
CREATE INDEX bounces_from_idx ON ses_notifications.bounces USING btree (source);
CREATE UNIQUE INDEX bounces_id_idx ON ses_notifications.bounces USING btree (id);
CREATE INDEX bounces_message_id_idx ON ses_notifications.bounces USING btree (message_id);
CREATE INDEX bounces_notification_id_idx ON ses_notifications.bounces USING btree (notification_id);
CREATE INDEX bounces_sent_at_idx ON ses_notifications.bounces USING btree (sent_at);

CREATE TABLE ses_notifications.complaints (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	notification_id int8 NOT NULL,
	notification_type varchar(32) NOT NULL,
	sent_at timestamptz NOT NULL,
	message_id varchar(128) NOT NULL,
	source varchar(256) NOT NULL COLLATE "ucs_basic",
	source_arn varchar(256) NULL,
	source_ip varchar(32) NULL,
	sending_account_id varchar(128) NULL,
	created_at timestamptz NULL,
	complaint_sub_type varchar(64) NULL COLLATE "ucs_basic",
	complaint_feedback_type varchar(256) NULL COLLATE "ucs_basic",
	feedback_id varchar(256) NULL,
	complained_recipients varchar(64000) NULL COLLATE "ucs_basic",
	user_agent varchar(1024) NULL,
	arrival_date timestamptz NULL
);
CREATE INDEX complaints_complained_recipients_idx ON ses_notifications.complaints (complained_recipients);
CREATE INDEX complaints_complaint_feedback_type_idx ON ses_notifications.complaints (complaint_feedback_type);
CREATE INDEX complaints_complaint_sub_type_idx ON ses_notifications.complaints (complaint_sub_type);
CREATE INDEX complaints_created_at_idx ON ses_notifications.complaints (created_at);
CREATE INDEX complaints_from_idx ON ses_notifications.complaints (source);
CREATE UNIQUE INDEX complaints_id_idx ON ses_notifications.complaints (id);
CREATE INDEX complaints_message_id_idx ON ses_notifications.complaints (message_id);
CREATE INDEX complaints_notification_id_idx ON ses_notifications.complaints (notification_id);
CREATE INDEX complaints_sent_at_idx ON ses_notifications.complaints (sent_at);

CREATE TABLE ses_notifications.deliveries (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	notification_id int8 NOT NULL,
	notification_type varchar(32) NOT NULL,
	sent_at timestamptz NOT NULL,
	message_id varchar(128) NOT NULL,
	source varchar(256) NOT NULL COLLATE "ucs_basic",
	source_arn varchar(256) NULL,
	source_ip varchar(32) NULL,
	sending_account_id varchar(128) NULL,
	delivered_at timestamptz NULL,
	smtp_response varchar(4000) NULL COLLATE "ucs_basic",
	reporting_mta varchar(256) NULL,
	remote_mta_ip varchar(32) NULL,
	recipients varchar(64000) NULL COLLATE "ucs_basic"
);
CREATE INDEX deliveries_delivered_at_idx ON ses_notifications.deliveries (delivered_at);
CREATE INDEX deliveries_from_idx ON ses_notifications.deliveries (source);
CREATE UNIQUE INDEX deliveries_id_idx ON ses_notifications.deliveries (id);
CREATE INDEX deliveries_message_id_idx ON ses_notifications.deliveries (message_id);
CREATE INDEX deliveries_notification_id_idx ON ses_notifications.deliveries (notification_id);
CREATE INDEX deliveries_recipients_idx ON ses_notifications.deliveries (recipients);
CREATE INDEX deliveries_sent_at_idx ON ses_notifications.deliveries (sent_at);
CREATE INDEX deliveries_smtp_response_idx ON ses_notifications.deliveries (smtp_response);

CREATE TABLE ses_notifications.notifications (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	notification varchar(64000) NOT NULL COLLATE "ucs_basic",
	received_at timestamptz NOT NULL,
	message_id varchar(128) NOT NULL,
	sent_at timestamptz NOT NULL
);
CREATE UNIQUE INDEX notifications_id_idx ON ses_notifications.notifications (id);
CREATE INDEX notifications_message_id_idx ON ses_notifications.notifications (message_id);
CREATE INDEX notifications_received_at_idx ON ses_notifications.notifications (received_at);
CREATE INDEX notifications_sent_at_idx ON ses_notifications.notifications (sent_at);

CREATE TABLE ses_notifications.monitorrules (
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	name varchar(100) NOT NULL,
	json_matcher varchar(500) NOT NULL,
	regex varchar(500) NOT NULL,
	ses_message varchar(100) NOT NULL
);
CREATE UNIQUE INDEX monitorrules_id_idx ON ses_notifications.monitorrules (id);

CREATE TABLE ses_notifications."configuration" (
	id int NOT NULL GENERATED ALWAYS AS IDENTITY,
	"key" varchar(100) NOT NULL,
	value varchar(4000) NOT NULL
);
CREATE UNIQUE INDEX configuration_id_idx ON ses_notifications."configuration" (id);
CREATE INDEX configuration_key_idx ON ses_notifications."configuration" ("key");

CREATE OR REPLACE VIEW ses_notifications.operational
AS SELECT bounceevents.notification_id,
    bounceevents.notification_type,
    bounceevents.sent_at,
    bounceevents.source,
    bounceevents.created_at,
    bounceevents.bounced_recipients AS recipients,
    bounceevents.bounce_type AS detail1,
    bounceevents.bounce_sub_type AS detail2
   FROM ses_notifications.bounceevents
UNION
 SELECT complaintevents.notification_id,
    complaintevents.notification_type,
    complaintevents.sent_at,
    complaintevents.source,
    complaintevents.created_at,
    complaintevents.complained_recipients AS recipients,
    complaintevents.complaint_sub_type AS detail1,
    complaintevents.complaint_feedback_type as detail2
   FROM ses_notifications.complaintevents
UNION
 SELECT deliveryevents.notification_id,
    deliveryevents.notification_type,
    deliveryevents.sent_at,
    deliveryevents.source,
    deliveryevents.delivered_at AS created_at,
    deliveryevents.recipients,
    NULL::character varying AS detail1,
    NULL::character varying AS detail2
   FROM ses_notifications.deliveryevents
UNION
 SELECT openevents.notification_id,
    openevents.notification_type,
    openevents.sent_at,
    openevents.source,
    openevents.opened_at AS created_at,
    openevents.recipients,
    NULL::character varying AS detail1,
    NULL::character varying AS detail2
   FROM ses_notifications.openevents;