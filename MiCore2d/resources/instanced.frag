#version 330

out vec4 outputColor;

in vec2 texCoord;
in float texIndex;

uniform sampler2DArray texture0;
uniform float texAlpha;

void main()
{
  if (texIndex < 0)
  {
    outputColor = vec4(0.0, 0.0, 0.0, 0.0);
  }
  else
  {
    outputColor = mix(texture(texture0, vec3(texCoord, texIndex)), vec4(0.0, 0.0, 0.0, 0.0), texAlpha);
  }
}
