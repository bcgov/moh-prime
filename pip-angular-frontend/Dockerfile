FROM public.ecr.aws/bitnami/nginx:1.21

COPY dist/pip-angular-frontend /opt/app-root/src

EXPOSE 80 443
CMD ["sh","-c","nginx -g 'daemon off;'"]