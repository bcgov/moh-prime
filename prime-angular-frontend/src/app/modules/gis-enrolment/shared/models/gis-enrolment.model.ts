import { BcscUser } from '@auth/shared/models/bcsc-user.model';

export class GisEnrolment implements Omit<BcscUser, 'verifiedAddress'> {
  constructor(
    public hpdid: string = null,
    public userId: string = null,
    public username: string = null,
    public givenNames: string = null,
    public dateOfBirth: string = null,
    public email: string = null,
    public firstName: string = null,
    public lastName: string = null,
    public ldapUsername: string = null,
    public ldapLoginSuccessDate: string = null,
    public phone: string = null,
    public organization: string = null,
    public role: string = null,
    public submittedDate: string = null,
    public id: number = 0
  ) {
    this.hpdid = hpdid;
    this.userId = userId;
    this.username = username;
    this.givenNames = givenNames;
    this.dateOfBirth = dateOfBirth;
    this.email = email;
    this.firstName = firstName;
    this.lastName = lastName;
    this.ldapUsername = ldapUsername;
    this.ldapLoginSuccessDate = ldapLoginSuccessDate;
    this.phone = phone;
    this.organization = organization;
    this.role = role;
    this.submittedDate = submittedDate;
    this.id = id;
  }

  public static fromBcscUser(bcscUser: BcscUser): GisEnrolment {
    return new GisEnrolment(
      bcscUser.hpdid,
      bcscUser.userId,
      bcscUser.givenNames,
      bcscUser.dateOfBirth,
      bcscUser.email,
      bcscUser.firstName,
      bcscUser.lastName
    );
  }

  public static toBcscUser(enrolment: GisEnrolment): Omit<BcscUser, 'verifiedAddress'> {
    const { hpdid, userId, username, givenNames, dateOfBirth, email, firstName, lastName } = enrolment;
    return { hpdid, userId, username, givenNames, dateOfBirth, email, firstName, lastName };
  }
}
