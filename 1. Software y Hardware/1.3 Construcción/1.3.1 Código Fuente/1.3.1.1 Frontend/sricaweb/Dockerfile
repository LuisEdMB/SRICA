FROM node:12 as build-step

RUN mkdir /app

WORKDIR /app

COPY ["1.3.1.1 Frontend/sricaweb", "/app"]

RUN npm install

RUN npm run build

FROM nginx:1.19.3

COPY --from=build-step /app/build /usr/share/nginx/html

COPY ["1.3.1.1 Frontend/sricaweb/nginx.conf", "/etc/nginx/conf.d/default.conf"]

EXPOSE 443

ENV TZ="America/Lima"