This document captures the solution evaluation criteria, and documents the way in which we believe our solution fulfils these criteria.

**Technical Requirements**

Solutions must be developed using technology and architectural style that demonstrates modern best practices.

The solution may leverage open source components and may include no new code if the requirements can be met through configuration. 

The solution must not include proprietary components.

Solutions must be deployed to a Docker container platform accessible to the Province (e.g.: OpenShift.io, Azure, Amazon Web Services, etc.). 

The solution must remain running and accessible from 8:30 a.m. till 5:00 p.m. Pacific Time on Tuesday, September 3. 

Dockerfiles must be committed to the private repository.

Solutions must be re-deployable using an automated deployment tool (e.g.: Jenkins, Azure DevOps, etc.). 

Configuration files for that tool must be committed to the private repository.

**Business Requirements**

Implement the following user stories:
    As an applicant, I want to authenticate using my Google account, so that I am identified on my enrolment application. Authentication must use OpenID Connect.
    As an applicant, I may want to provide my 4-digit pharmacist registration number, so that I can prove my credentials.
    As an applicant, I want to submit my application for enrolment, so that it can be processed.
    As an administrator, I want all applications with a registration number to be automatically approved, so that I don't waste my time reviewing them.
    As an administrator, I want to access an administrator interface using a separate URL, so that I can review applications. (The administrator does not need to be authenticated.)
    As an administrator, I want to see a list of all applications, so that I can review applications.
    As an administrator, I want to see if an application has been approved, so that I can choose to review it if not.


**Evaluation Criteria**

Meets requirements 	7
Architecture and technical design 	3
Code quality 	3
Code maintainability 	3
Technology choice 	3
Deployment artifacts 	2
Other artifacts 	2
Total 	23
