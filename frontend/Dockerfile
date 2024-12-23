FROM node:20-alpine
WORKDIR /app

COPY package*.json ./
RUN npm install -g @angular/cli@17
RUN npm ci
COPY . .

EXPOSE 4200
CMD ["ng", "serve", "--host", "0.0.0.0"]