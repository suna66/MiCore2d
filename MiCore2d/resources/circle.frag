#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture0;
uniform vec2 r;
uniform float radius;

void main()
{
  vec2 centor = r / 2.0;
  vec2 p = (texCoord * r) - centor;

  float len = length(p);
  if (len > radius)
  {
    outputColor = vec4(0.0, 0.0, 0.0, 0.0);
  }
  else
  {
    float alpha = len/radius;
    outputColor = mix(texture(texture0, texCoord), vec4(0.0, 0.0, 0.0, 0.0), alpha);
  }
}
