FROM registry.redhat.io/rhel8/s2i-core

ARG GITHUB_RUNNER_VERSION=${GITHUB_RUNNER_VERSION}
ARG GITHUB_PAT=${GITHUB_PAT}
ARG GITHUB_OWNER=${GITHUB_OWNER}
ARG GITHUB_REPOSITORY=${GITHUB_REPOSITORY}
ARG RUNNER_NAME=${RUNNER_NAME}

ENV RUNNER_NAME "${RUNNER_NAME}"
ENV GITHUB_PAT "${GITHUB_PAT}"
ENV GITHUB_OWNER "${GITHUB_OWNER}"
ENV GITHUB_REPOSITORY "${GITHUB_REPOSITORY}"
ENV RUNNER_WORKDIR "_work"

RUN yum update && \
    yum install -y \
        curl \
        sudo \
        git \
        jq && \
    mkdir -p /github
RUN mkdir -p /github
COPY *.sh /github

RUN cd /github && \
    curl -Ls https://github.com/actions/runner/releases/download/v${GITHUB_RUNNER_VERSION}/actions-runner-linux-x64-${GITHUB_RUNNER_VERSION}.tar.gz && \
    tar xzf actions-runner-linux-x64-${GITHUB_RUNNER_VERSION}.tar.gz && \
    ./bin/installdependencies.sh && \
    touch /github/.env && \
    touch /github/.path && \
    chmod -R 777 /github && \
    sudo chmod a+rwx /github 

USER github
WORKDIR /github

# RUN curl -Ls https://github.com/actions/runner/releases/download/v${GITHUB_RUNNER_VERSION}/actions-runner-linux-x64-${GITHUB_RUNNER_VERSION}.tar.gz | tar xz \
#     && sudo ./bin/installdependencies.sh

# COPY --chown=github:github entrypoint.sh ./entrypoint.sh
# RUN sudo chmod u+x ./entrypoint.sh

ENTRYPOINT ["/github/entrypoint.sh"]