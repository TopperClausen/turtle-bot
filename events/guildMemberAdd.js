module.exports = {
  execute: (guildMemberAdd, client) => {
    console.log(guildMemberAdd)
    const role = guildMemberAdd.guild.roles.cache.find(role => role.name === 'Teenage Turles');
    guildMemberAdd.addRole(role);
  }
}
