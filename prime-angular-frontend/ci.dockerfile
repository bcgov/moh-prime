FROM bitnami/nginx:1.21.5-debian-10-r3

COPY dist/angular-frontend/browser /opt/app-root/src

# USER 1001200000
EXPOSE 80 8080 4200:8080
CMD ["sh","-c","nginx -g 'daemon off;'"]
