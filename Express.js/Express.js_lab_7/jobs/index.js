const startHeartBeatJob = require('./heartbeat.job');
const usersLog = require('./usersLogReminder.job');

function start() {
    startHeartBeatJob();
    usersLog();
}

module.exports = start;