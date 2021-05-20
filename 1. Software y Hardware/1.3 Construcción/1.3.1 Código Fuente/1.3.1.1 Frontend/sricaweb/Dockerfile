FROM node:12 as build-step

RUN mkdir /app

WORKDIR /app

COPY ./package.json /app

RUN npm install

COPY . /app

RUN npm run build

FROM nginx:1.19.3

COPY --from=build-step /app/build /usr/share/nginx/html

COPY ./nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 443

ENV TZ="America/Lima"