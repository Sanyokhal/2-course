import mysql from 'mysql2'

const pool = mysql.createPool({
    host: '127.0.0.1',
    user: 'root',
    password: '',
    database: 'express_js_lab'
}).promise()

export async function getAllWorkers() {
    const [rows] = await pool.query("SELECT * FROM worker LEFT JOIN computer_data ON worker.pc_id = computer_data.id")
    return rows
}

export async function getWorkerById(id) {
    const [rows] = await pool.query("SELECT * FROM worker LEFT JOIN computer_data ON worker.pc_id = computer_data.id WHERE worker.id = ?", [id])
    return rows
}

export async function getPcDataList() {
    let [rows] = await pool.query("SELECT * FROM computer_data")
    return rows
}

export async function getPcDataById(id) {
    const [rows] = await pool.query("SELECT * FROM computer_data WHERE id = ?", [id])
    return rows
}

export async function removeWorkerById(id) {
    const [res] = await pool.query("DELETE FROM worker WHERE id = ?", [id])
    return res.affectedRows
}

export async function removeWorkerBySurname(surname) {
    const [res] = await pool.query("DELETE FROM worker WHERE surname = ? LIMIT 1", [surname])
    return res.affectedRows
}

export async function createNewWorker(surname, room_num, group_name, pc_id) {
    let [res] = await pool.query("INSERT INTO worker(surname,room_num,group_name,pc_id) VALUES(?,?,?,?)", [surname, room_num, group_name, pc_id])
    return res.insertId
}

export async function createNewPc(gpu, cpu, ram) {
    let [res] = await pool.query("INSERT INTO computer_data(gpu,cpu,ram) VALUES(?,?,?)", [gpu, cpu, ram])
    return res.insertId
}

export async function editWorkerById(id, surname, room_num, group_name, pc_id) {
    let [res] = await pool.query("UPDATE worker SET surname = ?, room_num = ?, group_name = ?, pc_id =? WHERE id = ?", [surname, room_num, group_name, pc_id, id])
    return res.affectedRows
}

export async function patchComputer(id, gpu, cpu, ram) {
    let [res] = await pool.query("UPDATE computer_data SET gpu = ?, cpu = ?, ram = ? WHERE id = ?", [gpu, cpu, ram, id])
    return res.affectedRows
}

// user actions

export async function createUser(username, password, first_name, last_name) {
    let [res] = await pool.query("INSERT INTO users(username,password,first_name,last_name) VALUES(?,?,?,?)", [username, password, first_name, last_name])
    return res.insertId
}

export async function getUser(username) {
    let [res] = await pool.query("SELECT id, username, password FROM users WHERE username LIKE ?", [`%${username}%`]);
    return res[0];
}
export async function getUsers() {
    let [res] = await pool.query("SELECT id, username, password FROM users");
    return res;
}
export async function checkUser(username) {
    let [res] = await pool.query("SELECT id, username, password FROM users WHERE username LIKE ?", [`%${username}%`]);
    if (res.length > 0) {
        return false
    }
    return true
}