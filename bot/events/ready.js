module.exports = {
  execute: (event, client) => {
    console.log(`Logged in as ${client.user.tag}!`);
    client.user.setActivity("TurlteBot", { type: "WATCHING" });
  }
}