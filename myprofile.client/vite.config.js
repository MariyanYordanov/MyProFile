import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import fs from "fs";
import path from "path";
import { fileURLToPath, URL } from "node:url";
import child_process from "child_process";
import { env } from "process";

const baseFolder =
    env.APPDATA !== undefined && env.APPDATA !== ""
        ? `${env.APPDATA}/ASP.NET/https`
        : `${env.HOME}/.aspnet/https`;

const certificateName = "myprofile.client";
const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

if (!fs.existsSync(baseFolder)) {
    fs.mkdirSync(baseFolder, { recursive: true });
}

if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
    if (
        0 !==
        child_process.spawnSync(
            "dotnet",
            [
                "dev-certs",
                "https",
                "--export-path",
                certFilePath,
                "--format",
                "Pem",
                "--no-password",
            ],
            { stdio: "inherit" }
        ).status
    ) {
        throw new Error("Could not create certificate.");
    }
}

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
        proxy: {
            "/api": {
                target: "https://localhost:7082",
                changeOrigin: true,
                secure: false,
            },
            "/uploads": {
                target: "https://localhost:7082",
                changeOrigin: true,
                secure: false,
            },
            "/profiles": {
                target: "https://localhost:7082",
                changeOrigin: true,
                secure: false,
            },
        },
    },
});
