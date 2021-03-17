import { BcscUser } from '@auth/shared/models/bcsc-user.model';

export class GisEnrolment implements Omit<BcscUser, 'verifiedAddress'> {
  constructor(
    public hpdid: string = null,
    public userId: string = null,
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
    public id?: number
  ) { }

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
}
