FROM node:20-alpine

WORKDIR /app

COPY package*.json .

RUN npm install express

COPY . .

EXPOSE 5173

CMD ["npm", "run", "dev"]