import os
import sys
import base64
import hashlib
from Crypto.Cipher import AES

# Load the key and IV from the file
key_file = open("key.keyzor", "rb").read()
key = key_file[:16]
iv = key_file[16:]

# Load the encrypted file
encrypted_file = open("encrypted.crypzor", "rb").read()

# Decrypt the encrypted file
cipher = AES.new(key, AES.MODE_CBC, iv)
decrypted = cipher.decrypt(encrypted_file)

# Remove padding
pad = decrypted[-1]
decrypted = decrypted[:-pad]

print("Decrypted text:", decrypted.decode())
input("Press Enter to continue...")

