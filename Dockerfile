FROM node:16

WORKDIR /
COPY ["bot/package.json", "bot/package-lock.json*", "./"]
RUN npm install
COPY / .
CMD ["node", "app.js"]
