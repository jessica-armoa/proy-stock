import Image from 'next/image';
 
export default function Photo({src}) {

  const fallbackSrc = "/404Tools.png"; // Replace with your fallback image URL or message
//<img src={imageSrc} onError={(e)=>{e.target.onError = null; e.target.src = fallbackSrc}}/>

  return (
    <>
      {src ? (
        <Image
          src={src} onError={(e)=>{ e.target.srcset=fallbackSrc; e.target.src=fallbackSrc;}}
          width={300}
          height={300}
          alt="Producto"
        />
      ) : (
        <div>No image available</div>
      )}
    </>
  )
}