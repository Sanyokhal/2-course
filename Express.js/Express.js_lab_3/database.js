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

export async function logRequest(method, url) {
    let [res] = await pool.query("INSERT INTO data_logs(route,method) VALUES(?,?)", [url, method]);
    return res.insertId
}
export async function getLogs(){
    let [res] = await pool.query("SELECT * FROM data_logs");
    return res
}