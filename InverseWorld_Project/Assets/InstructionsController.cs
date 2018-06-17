using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace VGDA.InverseWorld
{
	/// <summary>
    /// 
    /// 
    /// Michael Wolf
    /// 2018
    /// </summary>
	public class InstructionsController : InvertSubscriber
	{
	    public TextMeshPro textMesh;
	    public SpriteRenderer spriteRenderer;
	    public Color colorNormal;
	    public Color colorInverted;
	    public float delay = 1f;
	    public float blendDuration = 0.2f;

	    private bool hasBlendedIn = false;

	    protected override void Start()
	    {
	        base.Start();
	        spriteRenderer.material.color = new Color(
	            spriteRenderer.material.color.r,
	            spriteRenderer.material.color.g,
	            spriteRenderer.material.color.b,
	            0f
	        );
            textMesh.color = new Color(
                textMesh.color.r,
                textMesh.color.g,
                textMesh.color.b,
                0f
            );
            foreach (Renderer rend in GetComponentsInChildren<Renderer>())
	        {
	            
	        }
        }

	    protected override void DoInvert(bool isInverted)
	    {
	        var color = (isInverted) ? colorInverted : colorNormal;
	        textMesh.color = new Color(
	            color.r,
	            color.g,
	            color.b,
	            textMesh.color.a
	        );

        }

	    public void BlendIn()
	    {
	        if (!hasBlendedIn)
	        {
	            hasBlendedIn = true;
	            StartCoroutine(CoBlendIn(blendDuration));
	        }
	    }

	    private IEnumerator CoBlendIn(float duration)
	    {
	        //var renderers = GetComponentsInChildren<Renderer>();
            yield return new WaitForSecondsRealtime(delay);
	        float timer = 0f;
	        while (timer < duration)
	        {
	            timer += Time.deltaTime;
	            float lerp = timer / duration;
	            spriteRenderer.material.color = new Color(
	                spriteRenderer.material.color.r,
	                spriteRenderer.material.color.g,
	                spriteRenderer.material.color.b,
	                lerp
	            );
	            textMesh.color = new Color(
	                textMesh.color.r,
	                textMesh.color.g,
	                textMesh.color.b,
	                lerp
                );
             //   foreach (Renderer rend in renderers)
	            //{
	            //    rend.material.color = new Color(
	            //        rend.material.color.r,
	            //        rend.material.color.g,
	            //        rend.material.color.b,
             //           lerp * 255
             //           );
	            //}

	            yield return null;
	        }
	    }
	}
}