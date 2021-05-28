DO $$
DECLARE
	DECLARE vSAPartyId public."Party"."Id"%TYPE;
	DECLARE vSiteAddressID public."Address"."Id"%type;
	DECLARE vFakeAddressID public."Address"."Id"%type;
	DECLARE vRemoteUserID public."RemoteUser"."Id"%type;
	DECLARE vOrganizationID public."Organization"."Id"%type;
	DECLARE vSiteId public."Site"."Id"%type;
	DECLARE vCreatorUUID uuid := '00000000-0000-0000-0000-000000000000';
BEGIN
	--Create Fake Signing Authority Address
	INSERT INTO public."Address"
	("CreatedUserId", "CreatedTimeStamp", "UpdatedUserId", "UpdatedTimeStamp", "CountryCode", "ProvinceCode", "Street", "Street2", "City", "Postal", "AddressType")
	VALUES(vCreatorUUID, now(), vCreatorUUID, now(), 'CA', 'BC', '1515 Blanshard', null, 'Victoria', 'V8W 3C8', 1)
	RETURNING "Id" INTO vFakeAddressID;	
    --Create Fake Signing Authority
	INSERT INTO public."Party"
	("CreatedUserId", "CreatedTimeStamp", "UpdatedUserId", "UpdatedTimeStamp", "UserId", "HPDID", "FirstName", "LastName", "DateOfBirth", "JobRoleTitle", "Email", "Phone", "Fax", "SMSPhone", "PhysicalAddressId", "MailingAddressId", "MiddleName", "PreferredFirstName", "PreferredLastName", "PreferredMiddleName")
	VALUES(vCreatorUUID, now(), vCreatorUUID, now(), vCreatorUUID, null, 'Schedule B', 'Schedule B', '0001-01-01 00:00:00', 'Schedule B Fake User', 'PrimeSupport@gov.bc.ca', '5551231234', null, null, vFakeAddressID, null, null, null, null, null)
	RETURNING "Id" INTO vSAPartyId;
	--Create Fake Organization
	INSERT INTO public."Organization"
	("CreatedUserId", "CreatedTimeStamp", "UpdatedUserId", "UpdatedTimeStamp", "Name", "DoingBusinessAs", "SigningAuthorityId", "RegistrationId", "Completed", "SubmittedDate")
	VALUES(vCreatorUUID, now(), vCreatorUUID, now(), 'Schedule B Organization', 'Schedule B Organization', vSAPartyId, 'UNKNOWN', true, now())
	RETURNING "Id" INTO vOrganizationID;