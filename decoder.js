const crypto = require('crypto');
const fs = require('fs');

// Load the key and IV from the file
const keyFile = fs.readFileSync('key.keyzor');
const key = keyFile.slice(0, 16);
const iv = keyFile.slice(16, 32);

// Load the encrypted file
const encryptedFile = fs.readFileSync('encrypted.crypzor');

// Decrypt the encrypted file
const decipher = crypto.createDecipheriv('aes-128-cbc', key, iv);
let decrypted = decipher.update(encryptedFile);
decrypted = Buffer.concat([decrypted, decipher.final()]);
console.log('Decrypted text: ' + decrypted.toString());