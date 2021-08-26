DO $$
DECLARE
	DECLARE vSAPartyId public."Party"."Id"%TYPE;
	DECLARE vSiteAddressID public."Address"."Id"%type;
	DECLARE vFakeAddressID public."Address"."Id"%type;
	DECLARE vOrganizationID public."Organization"."Id"%type;
	DECLARE vSiteId public."Site"."Id"%type;
	DECLARE vCreatorUUID uuid := '00000000-0000-0000-0000-000000000000';
	DECLARE vUserId uuid := '00000000-0000-0000-0000-000000000011';
BEGIN
	--Create Fake Signing Authority Address
	INSERT INTO public."Address"
	("CreatedUserId", "CreatedTimeStamp", "UpdatedUserId", "UpdatedTimeStamp", "CountryCode", "ProvinceCode", "Street", "Street2", "City", "Postal", "AddressType")
	VALUES(vCreatorUUID, now(), vCreatorUUID, now(), 'CA', 'BC', '350 Columbia St', null, 'Vancouver', 'V6A 4J1', 1)
	RETURNING "Id" INTO vFakeAddressID;	
  --Create Fake Signing Authority
	INSERT INTO public."Party"
	("CreatedUserId", "CreatedTimeStamp", "UpdatedUserId", "UpdatedTimeStamp", "UserId", "HPDID", "FirstName", "LastName", "DateOfBirth", "JobRoleTitle", "Email", "Phone")
	VALUES(vCreatorUUID, now(), vCreatorUUID, now(), vUserId, null, 'Norma', 'Mackenzie', '0001-01-01 00:00:00', 'Administrator of PharmaNet Onboarding', 'PrimeSupport@gov.bc.ca', '5551231234')
	RETURNING "Id" INTO vSAPartyId;
	--Party Address Relationship
	INSERT INTO public."PartyAddress"
	("PartyId", "AddressId", "CreatedTimeStamp", "CreatedUserId", "UpdatedTimeStamp", "UpdatedUserId")
	VALUES(vSAPartyId, vFakeAddressID, now(), vCreatorUUID, now(), vCreatorUUID);
	--Create Fake Organization
	INSERT INTO public."Organization"
	("CreatedUserId", "CreatedTimeStamp", "UpdatedUserId", "UpdatedTimeStamp", "Name", "DoingBusinessAs", "SigningAuthorityId", "RegistrationId", "Completed", "SubmittedDate")
	VALUES(vCreatorUUID, now(), vCreatorUUID, now(), 'Portland Hotel Society', 'Portland Hotel Society', vSAPartyId, 'UNKNOWN', true, now())
	RETURNING "Id" INTO vOrganizationID;