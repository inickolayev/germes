FROM node:12-alpine as builder

WORKDIR /app
COPY package.json yarn.lock ./
RUN yarn install --frozen-lockfile

COPY . .
RUN yarn build

FROM node:12-alpine as runner

RUN yarn global add serve

WORKDIR /app
COPY --from=builder /app/build .

EXPOSE 3000

CMD ["serve", "-s", "build", "-l", "3000"]
