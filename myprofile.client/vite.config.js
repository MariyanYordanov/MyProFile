import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import fs from "fs";
import path from "path";
import { fileURLToPath, URL } from "node:url";

const baseFolder =
    process.env.APPDATA && process.env.APPDATA !== ""
        ? `${process.env.APPDATA}/ASP.NET/https`
        : `${process.env.HOME}/.aspnet/https`;

const certificateName = "myprofile.client";
const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

export default defineConfig({
    plugins: [react()],
    resolve: {
        alias: {
            "@": fileURLToPath(new URL("./src", import.meta.url)),
        },
    },
    server: {
        port: 5173,
        https: {
            key: fs.readFileSync(keyFilePath),
            cert: fs.readFileSync(certFilePath),
        },
        cors: true,
        proxy: {
            '/api': {
                target: 'https://localhost:7082',
                changeOrigin: true,
                secure: false
            },
        },
    }

});
