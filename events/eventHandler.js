const loadEvents = (client) => {
  client.on('ready', event => require('./ready').execute(event, client));
  client.on('guildMemberAdd', guildMemberAdd => require('./guildMemberAdd').execute(guildMemberAdd, client));
}

module.exports = { loadEvents }