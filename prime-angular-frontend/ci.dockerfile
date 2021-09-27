FROM public.ecr.aws/bitnami/nginx:1.21

COPY src/app/dist/angular-frontend /opt/app-root/src

# USER 1001200000
EXPOSE 80 8080 4200:8080
CMD ["sh","-c","nginx -g 'daemon off;'"]
