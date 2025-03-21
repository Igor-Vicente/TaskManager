FROM node:lts-alpine as build
# Define build arguments for environment variables
ARG VITE_BACKEND_DOMAIN
# Set environment variables during the build process
ENV VITE_BACKEND_DOMAIN=$VITE_BACKEND_DOMAIN

WORKDIR /app
COPY package*.json ./
RUN npm i

COPY . .
RUN npm run build

FROM nginx:latest as prod

COPY --from=build /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 80/tcp

CMD ["/usr/sbin/nginx", "-g", "daemon off;"]