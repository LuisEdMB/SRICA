import { useCallback, useEffect, useRef } from 'react'
import Compress from 'compress.js'

const compress = new Compress()

export const useCamera = () => {
    const cameraRef = useRef()
    const imageCapturer = useRef()

    useEffect(() => {
        const initCamera = async () => {
            const stream = await navigator.mediaDevices.getUserMedia({ video: { width: { exact: 1920 }, height: { exact: 1080 } }, audio: false })
            cameraRef.current.srcObject = stream
            cameraRef.current.play()

            const constraints = {
                advanced: [
                    {
                        'exposureTime': 5000,
                        'exposureMode': 'manual',
                        'contrast': 9,
                        'sharpness': 2,
                        'exposureCompensation': EsHoraDeLaManana() ? 150 : 170
                    }
                ]
            }

            const videoTrack = cameraRef.current.srcObject.getVideoTracks()[0]
            videoTrack.applyConstraints(constraints)

            imageCapturer.current = new ImageCapture(videoTrack)
        }
        initCamera()
    }, [])

    const takeImage = useCallback(async () => {
        const blob = await imageCapturer.current?.takePhoto(null)?.then(blob => blob)
        const file = new File([blob], "imagen")
        const imagen = await compress.compress([file], { quality: 0.75 }).then(data => data)
        return imagen[0]?.data || ""
    }, [])

    const EsHoraDeLaManana = () => {
        const fecha = new Date()
        const horaManana = new Date('1111-11-11T08:00:00')
        const horaTarde = new Date('1111-11-11T17:00:00')
        return fecha.getTime() >= horaManana && fecha.getTime() <= horaTarde
    }

    return [ cameraRef, takeImage ]
}