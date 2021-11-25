import moment from 'moment';

import { SiteStatusType } from '@lib/enums/site-status.enum';

import { BaseHealthAuthoritySite } from '@health-auth/shared/models/base-health-authority-site.model';
import { HealthAuthoritySiteUtils } from '@health-auth/shared/classes/health-authority-site-utils.class';

describe('HealthAuthoritySiteUtils', () => {
  const util = HealthAuthoritySiteUtils;

  /**
   * @description
   * Helper method for creating a base health authority site.
   */
  function createHealthAuthoritySite(
    updatedSiteProperties: Partial<BaseHealthAuthoritySite>
  ) {
    return {
      id: 0,
      healthAuthorityOrganizationId: 0,
      completed: null,
      submittedDate: null,
      approvedDate: null,
      status: null,
      ...updatedSiteProperties
    };
  }

  it('should exist', () => expect(util).toBeTruthy());

  describe('HealthAuthoritySiteUtils.isIncomplete', () => {
    it('should be an incomplete site', () => {
      const healthAuthoritySites = [
        {
          status: SiteStatusType.EDITABLE,
          submittedDate: null,
          approvedDate: null
        },
        {
          status: SiteStatusType.EDITABLE,
          submittedDate: moment().toString(),
          approvedDate: null
        }
      ].map(createHealthAuthoritySite);
      const results = healthAuthoritySites.map(HealthAuthoritySiteUtils.isIncomplete);
      expect(results.every(r => r)).toBeTrue();
    });

    it('should be a complete site', () => {
      const healthAuthoritySites = [
        {
          status: SiteStatusType.EDITABLE,
          submittedDate: moment().toString(),
          approvedDate: moment().toString()
        },
        {
          status: SiteStatusType.IN_REVIEW,
          submittedDate: moment().toString(),
          approvedDate: moment().toString()
        },
        {
          status: SiteStatusType.LOCKED,
          submittedDate: moment().toString(),
          approvedDate: moment().toString()
        }
      ].map(createHealthAuthoritySite);
      const results = healthAuthoritySites.map(HealthAuthoritySiteUtils.isIncomplete);
      expect(results.every(r => r)).toBeFalse();
    });
  });

  describe('HealthAuthoritySiteUtils.isInReview', () => {
    it('should be a site that is in review', () => {
      const healthAuthoritySite = createHealthAuthoritySite({ status: SiteStatusType.IN_REVIEW });
      const result = HealthAuthoritySiteUtils.isInReview(healthAuthoritySite);
      expect(result).toBeTrue();
    });

    it('should be a site that is not in review', () => {
      const healthAuthoritySites = [
        SiteStatusType.EDITABLE,
        SiteStatusType.LOCKED
      ].map(status => createHealthAuthoritySite({ status }));
      const results = healthAuthoritySites.map(HealthAuthoritySiteUtils.isInReview);
      expect(results.every(r => r)).toBeFalse();
    });
  });

  describe('HealthAuthoritySiteUtils.isLocked', () => {
    it('should be a site that is locked', () => {
      const healthAuthoritySite = createHealthAuthoritySite({ status: SiteStatusType.LOCKED });
      const result = HealthAuthoritySiteUtils.isLocked(healthAuthoritySite);
      expect(result).toBeTrue();
    });

    it('should be a site that is unlocked', () => {
      const healthAuthoritySites = [
        SiteStatusType.EDITABLE,
        SiteStatusType.IN_REVIEW
      ].map(status => createHealthAuthoritySite({ status }));
      const results = healthAuthoritySites.map(HealthAuthoritySiteUtils.isLocked);
      expect(results.every(r => r)).toBeFalse();
    });
  });

  describe('HealthAuthoritySiteUtils.isApproved', () => {
    it('should be a site that is approved', () => {
      const healthAuthoritySite = createHealthAuthoritySite({
        status: SiteStatusType.EDITABLE,
        approvedDate: moment().toString()
      });
      const result = HealthAuthoritySiteUtils.isApproved(healthAuthoritySite);
      expect(result).toBeTrue();
    });

    it('should be a site that is not approved', () => {
      const healthAuthoritySites = [
        {
          status: SiteStatusType.EDITABLE,
          approvedDate: null
        },
        {
          status: null,
          approvedDate: moment().toString()
        },
        {
          status: SiteStatusType.LOCKED,
          approvedDate: moment().toString()
        },
        {
          status: SiteStatusType.LOCKED,
          approvedDate: null
        },
        {
          status: SiteStatusType.IN_REVIEW,
          approvedDate: moment().toString()
        },
        {
          status: SiteStatusType.IN_REVIEW,
          approvedDate: null
        }
      ].map(createHealthAuthoritySite);
      const results = healthAuthoritySites.map(HealthAuthoritySiteUtils.isApproved);
      expect(results.every(r => r)).toBeFalse();
    });
  });

  describe('HealthAuthoritySiteUtils.withinRenewalPeriod', () => {
    // NOTE: withinRenewalPeriod does not require a set of tests as it directly uses
    // DateUtils.withinRenewalPeriod which is covered by it's own set of tests
  });

  describe('HealthAuthoritySiteUtils.getExpiryDate', () => {
    it('should be a site with an expiry date equal to the submitted date + 1 year', () => {
      const submittedDate = moment();
      const healthAuthoritySite = createHealthAuthoritySite({
        submittedDate: submittedDate.toString(),
        approvedDate: submittedDate.clone().add(10, 'days').toString()
      });
      const result = HealthAuthoritySiteUtils.getExpiryDate(healthAuthoritySite);
      expect(result).toEqual(submittedDate.clone().add(1, 'year').format());
    });
  });
});
