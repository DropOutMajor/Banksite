import { fileURLToPath, URL } from 'node:url';
import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import fs from 'fs';
import path from 'path';
import child_process from 'child_process';

const baseFolder = process.env.APPDATA && process.env.APPDATA !== ''
    ? `${process.env.APPDATA}/ASP.NET/https`
    : `${process.env.HOME}/.aspnet/https`;

const certificateArg = process.argv
    .map(arg => arg.match(/--name=(?<value>.+)/i))
    .filter(Boolean)[0];
const certificateName = certificateArg ? certificateArg.groups!.value : 'reactapp1.client';

if (!certificateName) {
    console.error('Invalid certificate name. Pass --name=<<app>>.');
    process.exit(-1);
}

const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
    const result = child_process.spawnSync(
        'dotnet',
        ['dev-certs', 'https', '--export-path', certFilePath, '--format', 'Pem', '--no-password'],
        { stdio: 'inherit' }
    );
    if (result.status !== 0) {
        throw new Error('Could not create certificate.');
    }
}

export default defineConfig({
    plugins: [react()],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url)),
        },
    },
    server: {
        https: {
            key: fs.readFileSync(keyFilePath),
            cert: fs.readFileSync(certFilePath),
        },
        port: 5173,
        proxy: {
            '/api': {
                target: 'https://localhost:7249',
                changeOrigin: true,
                secure: false,
            },
            '/paymentform': {
                target: 'https://localhost:7249',
                changeOrigin: true,
                secure: false,
            },
            '/bankdetails': {
                target: 'https://localhost:7249',
                changeOrigin: true,
                secure: false,
            },
            '/pingauth': {
                target: 'https://localhost:7249',
                changeOrigin: true,
                secure: false,
            },
            '/register': {
                target: 'https://localhost:7249',
                changeOrigin: true,
                secure: false,
            },
            '/login': {
                target: 'https://localhost:7249',
                changeOrigin: true,
                secure: false,
            },
            '/logout': {
                target: 'https://localhost:7249',
                changeOrigin: true,
                secure: false,
            },
        },
    },
});
